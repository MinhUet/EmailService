using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.Entities
{

    public class EmailTrackingTopic : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string EmailTrackingTopicID { get; set; }
        [BsonRequired]
        [BsonElement("EventTopicName")]
        public string EventTopicName { get; set; }
    }
}
