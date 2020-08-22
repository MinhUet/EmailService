using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

namespace BVMinh.EmailService.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            var appRepo = (IRepository<Application>)builder.Services.GetService(typeof(IRepository<Application>));
            Thread t = new Thread(async () =>
            {
                while (true)
                {
                    await GetApplications(appRepo);
                    await Task.Delay(1000);
                }
            });
            t.Start();
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var env = hostContext.HostingEnvironment;
                    var configuration = hostContext.Configuration;

                    services.Configure<EmailDataBaseSettings>(
					configuration.GetSection("EmailDataBaseSettings"));
					services.AddTransient<IDatabaseContext, MongoDatabaseContext>();
					services.AddTransient<IDatabaseSettings>(sp =>
						sp.GetRequiredService<IOptions<EmailDataBaseSettings>>().Value);

					services.AddTransient<IRepository<Application>, ApplicationRepository>();
					services.AddSingleton<IRepository<EmailTopic>, EmailTopicRepository>();
					services.AddTransient<IRepository<SendMailSuccess>, SendMailSuccessRepo>();
					//Kafka  30/6
					var consumerConfig = new ConsumerConfig();
					configuration.Bind("Kafka:Consumer", consumerConfig);
					services.AddSingleton<ConsumerConfig>(consumerConfig);

					var producerConfig = new ProducerConfig();
					configuration.Bind("Kafka:Producer", producerConfig);
					services.AddSingleton<ProducerConfig>(producerConfig);

					services.AddSingleton<ServiceProviderResolver>();
					services.AddHostedService<Worker>();
                });

        private static async Task GetApplications(IRepository<Application> appRepo)
        {
            var apps = await appRepo.GetAll();
            Worker.AppCodes = apps.ToList();
        }
    }
}
