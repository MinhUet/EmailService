using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BVMinh.EmailService.Common.Kafka;
using BVMinh.EmailService.Common.Utilities;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.DTO;
using BVMinh.EmailService.Entity.Entities;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BVMinh.EmailService.Rescheduler
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly ProducerConfig _producerConfig;
        private readonly SendMailFailRepo _sendMailFail;
        private readonly string _sendMailRetry;
        private readonly string _shareTopic;
        private readonly int _maxRetrySendMailTimes;

        public Worker(IConfiguration configuration,
                      ILogger<Worker> logger,
                      ConsumerConfig consumerConfig,
                      ProducerConfig producerConfig,
                      IRepository<SendMailFail> sendMailFail)
        {
            _logger = logger;
            _configuration = configuration;
            _consumerConfig = consumerConfig;
            _producerConfig = producerConfig;
            _sendMailFail = (SendMailFailRepo)sendMailFail;
            _shareTopic = _configuration.GetValue<string>("EmailPacket:ShareTopic");
            _sendMailRetry = _configuration.GetValue<string>("EmailPacket:SendMailRetry");
            _maxRetrySendMailTimes = _configuration.GetValue<int>("EmailPacket:MaxRetrySendMailTimes");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Listening topic: " + _sendMailRetry);
                var t = new Thread(async () =>
                {
                    Console.WriteLine("Read at :" + DateTime.UtcNow);
                    _consumerConfig.GroupId = _sendMailRetry;
                    using (var _consumer = new ConsumerWrapper<Null, string>(_consumerConfig, _sendMailRetry))
                    {
                        while (true)
                        {
                            try
                            {
                                var message = _consumer.ReadMessage();
                                if (message != null)
                                {
                                    var messageJObj = JsonConvert.DeserializeObject<OutboxEmailDTO>(message);
                                    var nametopic = (messageJObj.IsShare == true) ? _shareTopic : messageJObj.ApplicationCode.Trim().ToString();
                                    if (messageJObj.RetryTime < _maxRetrySendMailTimes)
                                    {
                                        messageJObj.RetryTime++;
                                        var messageRetry = JsonConvert.SerializeObject(messageJObj);
                                        using (var producer = new ProducerWrapper<Null, string>(_producerConfig, nametopic))
                                        {
                                            await producer.SendMessage(messageRetry);
                                            Console.WriteLine("Retry into kafka");
                                            _logger.LogInformation("Send retry ShareTopic or AppCodeName: ");
                                        }
                                    }
                                    else
                                    {
                                        SendMailFail mailFail = new SendMailFail();
                                        Utility.CopyObject<SendMailFail>(messageJObj, ref mailFail);
                                        var a = mailFail;
                                        string result = await _sendMailFail.Insert(mailFail);
                                        Console.WriteLine("Save db success with mail fail");
                                        _logger.LogInformation("Save db succcess with send mail fail: " + result);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation(ex.ToString());
                            }
                        }
                    }
                });
                t.Start();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
