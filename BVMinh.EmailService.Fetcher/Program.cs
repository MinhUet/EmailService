using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.ConfigObjects;
using BVMinh.EmailService.Entity.Entities;
using BVMinh.EmailService.Entity.Interfaces;

namespace BVMinh.EmailService.Fetcher
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
                    var configuration = hostContext.Configuration;
                    services.Configure<EmailDataBaseSettings>(configuration.GetSection("EmailDataBaseSettings"));

                    services.AddTransient<IDatabaseSettings>(sp =>
                        sp.GetRequiredService<IOptions<EmailDataBaseSettings>>().Value);

                    services.AddTransient<IDatabaseContext, MongoDatabaseContext>();
                    services.AddTransient<IRepository<Application>, ApplicationRepository>();
                    services.AddHostedService<Worker>();
                });
    }
}
