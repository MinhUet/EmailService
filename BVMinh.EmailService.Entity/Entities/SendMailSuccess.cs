using BVMinh.EmailService.Entity.DTO;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.Entities
{
    public class SendMailSuccess : BaseMail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string SendMailSuccessID { get; set; }
    }
}
