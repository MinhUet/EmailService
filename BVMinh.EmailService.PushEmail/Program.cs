using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.ConfigObjects;
using BVMinh.EmailService.Entity.Entities;
using BVMinh.EmailService.Entity.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BVMinh.EmailService.PushEmail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var env = hostContext.HostingEnvironment;
                    var configuration = hostContext.Configuration;

                    services.Configure<EmailDataBaseSettings>(configuration.GetSection("EmailDataBaseSettings"));

                    services.AddTransient<IDatabaseSettings>(sp =>
                        sp.GetRequiredService<IOptions<EmailDataBaseSettings>>().Value);

                    services.AddTransient<IDatabaseContext, MongoDatabaseContext>();
                    services.AddSingleton<IRepository<SchedulerTopic>, SchedulerTopicRepository>();
                    services.AddSingleton<IRepository<EmailTopic>, EmailTopicRepository>();

                    //Kafka consumer
                    var consumerConfig = new ConsumerConfig();
                    configuration.Bind("Kafka:Consumer", consumerConfig);

                    services.AddSingleton<ConsumerConfig>(consumerConfig);

                    //Kafka producer
                    var producerConfig = new ProducerConfig();
                    configuration.Bind("Kafka:Producer", producerConfig);

                    services.AddSingleton<ProducerConfig>(producerConfig);

                    services.AddHostedService<Worker>();
                });
    }
}
