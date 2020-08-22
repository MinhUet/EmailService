using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
    public class SchedulerTopicRepository : BaseRepository<SchedulerTopic>
    {
        public SchedulerTopicRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public List<SchedulerTopic> GetEmailFilter(int limitRecords, int minutes)
        {
            FilterDefinition<SchedulerTopic> filter;
            FilterDefinitionBuilder<SchedulerTopic> builder;
            builder = Builders<SchedulerTopic>.Filter;

            // Lấy các email sắp gửi trong minutes phút tới
            filter = builder.Gte(x => x.ScheduleTime, DateTime.Now) & builder.Lt(x => x.ScheduleTime, DateTime.Now.AddMinutes(minutes)) & builder.Eq("ProcessingStatus", false);

            List<SchedulerTopic> result = _collection.Find(filter).Limit(limitRecords).ToList();

            return result;
        }

     
        public long GetNumberSchedulingEmail(int minutes)
        {
            FilterDefinition<SchedulerTopic> filter;
            FilterDefinitionBuilder<SchedulerTopic> builder;
            builder = Builders<SchedulerTopic>.Filter;
            // Lấy các email sắp gửi trong minutes phút tới
            filter = builder.Gte(x => x.ScheduleTime, DateTime.Now) & builder.Lt(x => x.ScheduleTime, DateTime.Now.AddMinutes(minutes)) & builder.Eq("ProcessingStatus", false);

            return _collection.Find(filter).ToList().Count();
        }


        public void UpdateStatus(List<SchedulerTopic> schedulerTopics)
        {
            List<string> emailList = new List<string>();

            foreach (var schedulerTopic in schedulerTopics)
            {
                string emailID = schedulerTopic.SchedulerTopicID;
                emailList.Add(emailID);
            }

            string[] emailArray = emailList.ToArray();

            var update = Builders<SchedulerTopic>.Update.Set(a => a.ProcessingStatus, true);
            FilterDefinition<SchedulerTopic> filter = Builders<SchedulerTopic>.Filter.In("SchedulerTopicID", emailArray);

            _collection.UpdateMany(filter, update);
        }

        
        public void DeleteEmail(List<SchedulerTopic> schedulerTopics)
        {
            List<string> emailList = new List<string>();

            foreach (var schedulerTopic in schedulerTopics)
            {
                string emailID = schedulerTopic.SchedulerTopicID;
                emailList.Add(emailID);
            }

            string[] emailArray = emailList.ToArray();
            FilterDefinition<SchedulerTopic> filter = Builders<SchedulerTopic>.Filter.In("SchedulerTopicID", emailArray);
            _collection.DeleteMany(filter);
        }

        public string DeleteManyEmailScheduler(List<string> emailList)
        {
            try
            {
                FilterDefinition<SchedulerTopic> filter = Builders<SchedulerTopic>.Filter.In("EmailID", emailList);
                _collection.DeleteMany(filter);
                return "DeleteSuccess";
            }
            catch (Exception ex)
            {
                return "Delete fail :" + ex;
            }
        }
    }
}
