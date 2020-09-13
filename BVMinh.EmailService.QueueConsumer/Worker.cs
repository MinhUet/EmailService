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

namespace BVMinh.EmailService.QueueConsumer
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly SchedulerTopicRepository _schedulerTopicRepo;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, ConsumerConfig consumerConfig, IRepository<SchedulerTopic> schedulerTopicRepo)
        {
            _configuration = configuration;
            _logger = logger;
            _consumerConfig = consumerConfig;
            _schedulerTopicRepo = (SchedulerTopicRepository)schedulerTopicRepo;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string topicName = _configuration["EmailPacket:ShedulerTopic"];
            _logger.LogInformation("- Listening to topic " + topicName + "...");
            _consumerConfig.GroupId = topicName;

            using (var _consumer = new ConsumerWrapper<Null, string>(_consumerConfig, topicName))
            {
                _logger.LogInformation("- Connected.");
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var message = _consumer.ReadMessage();
                        if (message == null)
                        {
                            continue;
                        }
                        _logger.LogInformation("- Get: " + message);
                        await StoreMessage(message);
                        _logger.LogInformation("- Store successful.");
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    await Task.Delay(0, stoppingToken);
                }

            }
        }

        public async Task StoreMessage(string message)
        {

            var schedulerTopic = JsonConvert.DeserializeObject<SchedulerTopic>(message);

            await _schedulerTopicRepo.Insert(schedulerTopic);
        }
    }
}
