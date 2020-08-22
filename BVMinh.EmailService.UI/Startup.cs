using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BVMinh.EmailService.API.Controllers;
using BVMinh.EmailService.API.EmailLogics;
using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.ConfigObjects;
using BVMinh.EmailService.Entity.Entities;
using BVMinh.EmailService.Entity.Interfaces;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BVMinh.EmailService.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // requires using Microsoft.Extensions.Options
            services.Configure<EmailDataBaseSettings>(Configuration.GetSection(nameof(EmailDataBaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<EmailDataBaseSettings>>().Value);
            services.AddTransient<IDatabaseContext, MongoDatabaseContext>();

            //Kafka  30/6
            var consumerConfig = new ConsumerConfig();
            Configuration.Bind("Kafka:Consumer", consumerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);
            
            var producerConfig = new ProducerConfig();
            Configuration.Bind("Kafka:Producer", producerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);

            services.AddSingleton<EmailLogic>();
            services.AddScoped<IRepository<EmailTopic>, EmailTopicRepository>();
            services.AddScoped<IRepository<Application>, ApplicationRepository>();
            services.AddScoped<IRepository<SchedulerTopic>, SchedulerTopicRepository>();
            services.AddScoped<IRepository<EmailTrackingTopic>, EmailTrackingTopicRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var appRepo = (IRepository<Application>)serviceProvider.GetService(typeof(IRepository<Application>));
            Thread t = new Thread(async () => {
                while (true)
                {
                    await GetApplications(appRepo);
                    await Task.Delay(1000);
                }
            });
            t.Start();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private async Task GetApplications(IRepository<Application> appRepo)
        {
            var apps = await appRepo.GetAll();
            EmailController._applications = apps.ToList();
        }
    }
}
