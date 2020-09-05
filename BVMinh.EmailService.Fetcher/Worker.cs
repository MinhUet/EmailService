using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace BVMinh.EmailService.Fetcher
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRepository<Application> _appRepo;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IRepository<Application> repository, IConfiguration configuration)
        {
            _logger = logger;
            _appRepo = repository;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await GetApplication(stoppingToken);
            await Task.Delay(0);
        }

       
        private async Task GetApplication(CancellationToken stoppingToken)
        {
            int minutes = _configuration.GetValue<int>("Redis:QueryDatabaseCyclingTime"); // sau "minutes" phút thì query DB 1 lần
            string redisIPConfig = (_configuration.GetSection("Redis:RedisIPConfig").Value); // Địa chỉ IP Server ( Kết nối tới Redis)
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var redis = ConnectionMultiplexer.Connect(redisIPConfig))
                {
                    try
                    {
                        IDatabase db = redis.GetDatabase();
                        var tmpResults = await _appRepo.GetAll();
                        var results = tmpResults.ToList();
                        foreach (var result in results)
                        {
                            _logger.LogInformation($"Set {result.ApplicationCode}");
                            db.StringSet(result.ApplicationCode, JsonConvert.SerializeObject(result));
                        }
                        redis.Close();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Redis Fetch error at: " + ex);
                    }
                }
                await Task.Delay(minutes * 1000 * 60); // Delay số phút là minutes
            }
        }
    }
}
