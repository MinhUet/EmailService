using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
	public class EmailTrackingTopicRepository : BaseRepository<EmailTrackingTopic>
	{
		public EmailTrackingTopicRepository(IDatabaseContext dbContext) : base(dbContext)
		{
		}
		public IEnumerable<EmailTrackingTopic> GetTopics()
		{
			return _collection.AsQueryable().ToList();
		}
	}
}
