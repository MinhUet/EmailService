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
using BVMinh.EmailService.Common.Redis;


namespace BVMinh.EmailService.Rescheduler
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
                    services.AddHostedService<Worker>();
                    var configuration = hostContext.Configuration;
                    services.Configure<EmailDataBaseSettings>(configuration.GetSection("EmailDataBaseSettings"));
                    //configuration.GetSection("EmailDataBaseSettings").Get<EmailDataBaseSettings>();

                    services.AddTransient<IDatabaseSettings>(sp =>
                        sp.GetRequiredService<IOptions<EmailDataBaseSettings>>().Value);

                    services.AddTransient<IDatabaseContext, MongoDatabaseContext>();
                    services.AddSingleton<IRepository<SendMailFail>, SendMailFailRepo>();
                    //Kafka  30/6
                    var consumerConfig = new ConsumerConfig();
                    configuration.Bind("Kafka:Consumer", consumerConfig);

                    var producerConfig = new ProducerConfig();
                    configuration.Bind("Kafka:Producer", producerConfig);

                    GetApplication._connectionString = configuration.GetValue<string>("Redis:ConnectionString");

                    services.AddSingleton<ConsumerConfig>(consumerConfig);
                    services.AddSingleton<ProducerConfig>(producerConfig);
                    services.AddHostedService<Worker>();
                });
    }
}
