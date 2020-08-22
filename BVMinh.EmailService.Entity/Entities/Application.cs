using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.Entities
{
   
    public class Application : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationCode { get; set; }
        public string CompanyCode { get; set; }
        public string WebHookUrl { get; set; }
        public string EmailServerAddress { get; set; }
        public string EmailServerPort { get; set; }
        public string EmailUserName { get; set; }
        public string EmailUserPassword { get; set; }
        public string EmailServiceProvider { get; set; }
        //Tên người gửi
        public string EmailSenderName { get; set; }
        //Địa chỉ người gửi
        public string EmailSenderAddress { get; set; }
        public string EmailReplyToAddress { get; set; }

    }
}
