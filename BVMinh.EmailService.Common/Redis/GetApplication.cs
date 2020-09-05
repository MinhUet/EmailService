using System;
using System.Collections.Generic;
using System.Text;
using BVMinh.EmailService.Entity.Entities;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BVMinh.EmailService.Common.Redis
{
    public class GetApplication
    {
        public static string _connectionString;
        
		public static Application GetApp(string key)
        {
            using (var redis = ConnectionMultiplexer.Connect(_connectionString))
            {

                try
                {
                    IDatabase db = redis.GetDatabase();
                    var res = db.StringGet(key);

                    if (res.IsNull)
                    {
                        Console.WriteLine("Redis App Code is null");
                        return null;
                    }
                    else
                    {
                        Application appCode = JsonConvert.DeserializeObject<Application>(res);
                        //Console.WriteLine(appCode.ApplicationCode);
                        return appCode;
                    }
                }
                catch
                {
                    Console.WriteLine("GET Redis App Code is failed");
                    return null;
                }
            }
        }

        public static async Task<string> GetString(string key)
        {
            using (var redis = ConnectionMultiplexer.Connect(_connectionString))
            {

                try
                {
                    IDatabase db = redis.GetDatabase();
                    var res = await db.StringGetAsync(key);

                    if (res.IsNull)
                    {
                        Console.WriteLine("Redis App Code is null");
                        return null;
                    }
                    else
                    {
                        return res.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Redis Get String failed: " + ex);
                    return null;
                }
            }
        }

        public static async Task<bool> SetString(string key, string value, TimeSpan? expiry)
        {
            using (var redis = ConnectionMultiplexer.Connect(_connectionString))
            {

                try
                {
                    IDatabase db = redis.GetDatabase();
                    var res = await db.StringSetAsync(key, value, expiry ?? TimeSpan.FromMinutes(30));

                    return res;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Redis Set String failed: " + ex);
                    return false;
                }
            }
        }
    }
}
