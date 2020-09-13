using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BVMinh.EmailService.Common.Kafka;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.Entities;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BVMinh.EmailService.PushEmail
{
    public class Worker : BackgroundService
    {
        private readonly object queryLock = new Object();

        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly ProducerConfig _producerConfig;
        private readonly SchedulerTopicRepository _schedulerTopicRepo;
        private List<EmailTopic> _topics;
        private readonly EmailTopicRepository _emailTopicRepo;

        public Worker(
            IConfiguration configuration, 
            ILogger<Worker> logger, 
            ProducerConfig producerConfig, 
            IRepository<EmailTopic> emailTopicRepo, 
            IRepository<SchedulerTopic> schedulerTopicRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _producerConfig = producerConfig;
            _schedulerTopicRepo = (SchedulerTopicRepository)schedulerTopicRepo;
            _emailTopicRepo = (EmailTopicRepository)emailTopicRepo;
            _topics = new List<EmailTopic>();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var threads = new List<Thread>();
            int numThread = _configuration.GetValue<int>("Threading:Amount"); // Số thread

            for (int i = 0; i < numThread; i++)
            {
                var t = new Thread(async () =>
                {
                    await ReadDatabase(stoppingToken);
                });

                threads.Add(t);
            }

            for (int i = 0; i < numThread; i++)
            {
                _logger.LogInformation("Thread " + i + " started.");
                threads[i].Start();
            }

            await Task.Delay(0);
        }

        public async Task ReadDatabase(CancellationToken stoppingToken)
        {
            long count; // số bản ghi truy vấn được
            int limitRecords = _configuration.GetValue<int>("Email:LimitRecords"); // số bản ghi đọc vào từ DB trong một lần
            int minutes = _configuration.GetValue<int>("EmailPacket:QueryDatabaseCyclingTime"); // số phút delay
            int maxItemsPerPush = _configuration.GetValue<int>("Kafka:MaxItemsPerPush"); // số message đẩy vào topic trong một lần

            while (!stoppingToken.IsCancellationRequested)
            {
                lock (queryLock)
                {
                    count = _schedulerTopicRepo.GetNumberSchedulingEmail(minutes);
                }

                if (count > 0)
                {
                    while (count > 0)
                    {
                        _logger.LogInformation("Thread detects " + count + " emails.");

                        var schedulerTopics = new List<SchedulerTopic>();

                        lock (queryLock)
                        {
                            schedulerTopics = _schedulerTopicRepo.GetEmailFilter(limitRecords, minutes);

                            _schedulerTopicRepo.UpdateStatus(schedulerTopics);

                        }

                        await PushIntoKafkaForSend(schedulerTopics, maxItemsPerPush);

                        _schedulerTopicRepo.DeleteEmail(schedulerTopics);

                        count -= maxItemsPerPush;
                    }
                }
                else
                {
                    _logger.LogInformation("There is no email for send!");
                }

                await Task.Delay(minutes * 60 * 1000); // Delay minutes phút 
            }
        }

        private async Task PushIntoKafkaForSend(List<SchedulerTopic> schedulerTopics, int maxItemsPerPush)
        {


            // Nhóm theo IsShare
            var groupByIsShare = from schedulerTopic in schedulerTopics group schedulerTopic by schedulerTopic.IsShare;

            foreach (var shareGroup in groupByIsShare)
            {
                _logger.LogInformation(shareGroup.Key.ToString());
                // Nếu IsShare == true
                // Đẩy vào topic ShareTopic
                if (shareGroup.Key == true)
                {
                    var shareTopicName = _configuration["EmailPacket:ShareTopic"];
                    using (var producer = new ProducerWrapper<Null, string>(_producerConfig, shareTopicName))
                    {
                        long numberOfItems = shareGroup.Count();
                        // Lấy từng cụm 50 email
                        for (int i = 0; i < numberOfItems; i += maxItemsPerPush)
                        {
                            var emails = shareGroup.Skip(i).Take(maxItemsPerPush);

                            foreach (var email in emails)
                            {
                                await producer.SendMessage(JsonConvert.SerializeObject(email));
                            }
                        }
                    }
                }

                // Nếu không thì đẩy sang topic tương ứng với ApplicationCode
                else
                {
                    // Nhóm theo application code
                    var queryEmails = from schedulerTopic in shareGroup group schedulerTopic by schedulerTopic.ApplicationCode;

                    foreach (var emailGroup in queryEmails)
                    {
                        using (var producer = new ProducerWrapper<Null, string>(_producerConfig, emailGroup.Key))
                        {
                            long numberOfItems = emailGroup.Count();
                            // Lấy từng cụm 50 email
                            for (int i = 0; i < numberOfItems; i += maxItemsPerPush)
                            {
                                var emails = emailGroup.Skip(i).Take(maxItemsPerPush);

                                foreach (var email in emails)
                                {
                                    await producer.SendMessage(JsonConvert.SerializeObject(email));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
