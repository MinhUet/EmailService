using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.Entities
{
    public class OutboxEmail : BaseEntity, IDisposable
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string OutboxEmailID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Boolean IsBodyHtml { get; set; }
        public string ApplicationCode { get; set; }
        public string EmailSenderAddress { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Array)]
        public List<EmailRecipient> Recipients { get; set; }
        //Có merge data từ recipient vào email không
        public Boolean IsMerge { get; set; }
        //Số lần thử lại
        public int RetryCount { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
