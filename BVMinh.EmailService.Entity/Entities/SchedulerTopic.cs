using BVMinh.EmailService.Entity.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.Entities
{
    public class SchedulerTopic : OutboxEmailDTO
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string SchedulerTopicID { get; set; }
        public bool ProcessingStatus { get; set; }
    }
}
