using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
    public class EmailTopicRepository : BaseRepository<EmailTopic>
    {
        public EmailTopicRepository(IDatabaseContext dbContext) : base(dbContext)
        {

        }
        public IEnumerable<EmailTopic> All => _collection.AsQueryable().ToList();
    }
}
