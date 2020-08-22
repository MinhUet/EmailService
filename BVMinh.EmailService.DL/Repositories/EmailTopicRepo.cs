using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
    public class EmailTopicRepo : BaseRepository<EmailTopic>
    {
        public EmailTopicRepo(IDatabaseContext dbContext) : base(dbContext)
        {

        }
        public IEnumerable<EmailTopic> GetAll()
        {
            return _collection.AsQueryable().ToList();

        }
    }
}
