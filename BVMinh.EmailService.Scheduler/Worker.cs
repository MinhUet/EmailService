using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using BVMinh.EmailService.Common.Interfaces;
using BVMinh.EmailService.Common.Kafka;
using BVMinh.EmailService.Common.Utilities;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.DTO;
using BVMinh.EmailService.Entity.Entities;
using BVMinh.EmailService.Scheduler.EmailLogics;
using BVMinh.EmailService.Scheduler.EmailProvider;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BVMinh.EmailService.Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly ProducerConfig _producerConfig;
        private readonly IRepository<EmailTopic> _emailTopicRepo;
        private readonly IRepository<SendMailSuccess> _sendMailSuccessRepo;
        private IEmailSender<OutboxEmailDTO> _emailSender;
        private readonly EmailLogic _emailLogic;
        private List<EmailTopic> _topics;
        public static List<Application> AppCodes;

        public Worker(IConfiguration configuration,
            ILogger<Worker> logger,
            ConsumerConfig consumerConfig,
            ProducerConfig producerConfig,
            IRepository<EmailTopic> emailTopicRepo,
            IRepository<SendMailSuccess> sendMailSuccessRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _emailTopicRepo = emailTopicRepo;
            _topics = new List<EmailTopic>();
            _consumerConfig = consumerConfig;
            _producerConfig = producerConfig;
            _sendMailSuccessRepo = sendMailSuccessRepo;
            _emailLogic = new EmailLogic(_configuration);
        }
       
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var tmpTopics = await _emailTopicRepo.GetAll();
                IEnumerable<EmailTopic> topics = tmpTopics.ToList();

                foreach (var topic in topics)
                {
                    if (!_topics.Exists(et => et.EmailTopicID == topic.EmailTopicID))
                    {
                        Console.WriteLine("Listening to topic:" + topic.EmailTopicName);
                        var t = new Thread(async () =>
                        {
                            _consumerConfig.GroupId = topic.EmailTopicName;
                            using (var _consumer = new ConsumerWrapper<Null, string>(_consumerConfig, topic.EmailTopicName))
                            {
                                while (true)
                                {
                                    try
                                    {
                                        var message = _consumer.ReadMessage();
                                        Console.WriteLine("Read at :" + DateTime.UtcNow);
                                        if (message == null)
                                        {
                                            continue;
                                        }
                                        var messageJObj = JsonConvert.DeserializeObject<OutboxEmailDTO>(message);
                                        var _application = AppCodes.Find(app => app.ApplicationCode == messageJObj.ApplicationCode);

                                        _emailSender = new EmailSenderSMTP(_configuration, _application.EmailUserName, _application.EmailUserPassword);

                                        if (_application.EmailServiceProvider == "SendGrid")
                                        {
                                            _emailSender = new EmailSenderSendgrid(_configuration, _application.EmailUserName, _application.EmailUserPassword);
                                        }
										//Chia  mail thanh tung package moi package gom 250 recipients
                                        List<OutboxEmailDTO> EmailSendIntoKafkas = new List<OutboxEmailDTO>();

                                        EmailSendIntoKafkas = _emailLogic.DivisionEmail(messageJObj);

                                        foreach (var emailPacket in EmailSendIntoKafkas)
                                        {
                                            //if (emailPacket.IsMerge)
                                            //{
                                            //    var listRecipientSendMailFail = await _emailSender.SendMailMerge(emailPacket);
                                            //    if (listRecipientSendMailFail.Count() == 0)
                                            //    {
                                            //        SaveSendMailSuccess(emailPacket);
                                            //    }
                                            //    else
                                            //    {
                                            //        foreach (var recipientSendMailFail in listRecipientSendMailFail)
                                            //        {
                                            //            emailPacket.Recipients.Remove(recipientSendMailFail);
                                            //        }
                                            //        SaveSendMailSuccess(emailPacket);

                                            //        emailPacket.Recipients = listRecipientSendMailFail;
                                            //        SendMsgIntoKafka(emailPacket);
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    if (await _emailSender.SendMailNoMerge(emailPacket))
                                            //    {
                                            //        SaveSendMailSuccess(emailPacket);
                                            //    }
                                            //    else
                                            //    {
                                            //        SendMsgIntoKafka(emailPacket);
                                            //    }
                                            //}
                                            SendMsgIntoKafka(emailPacket);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogInformation("Scheduler error at: " + ex);
                                    }
                                }
                            }
                        });
                        t.Start();
                        _topics.Add(topic);
                    }
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
        
        private async void SaveSendMailSuccess(OutboxEmailDTO emailPacket)
        {
            var mailSuccess = new SendMailSuccess();
            Utility.CopyObject<SendMailSuccess>(emailPacket, ref mailSuccess);
            await _sendMailSuccessRepo.Insert(mailSuccess);
            _logger.LogInformation("Succcess: " + emailPacket);
        }

        private async void SendMsgIntoKafka(OutboxEmailDTO emailPacket)
        {
            var nameTopic = "SendMailRetry";
            using (var producer = new ProducerWrapper<Null, string>(_producerConfig, nameTopic))
            {
                try
                {
                    await producer.SendMessage(JsonConvert.SerializeObject(emailPacket));
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Fail : " + ex);
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
