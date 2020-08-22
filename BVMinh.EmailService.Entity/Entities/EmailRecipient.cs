using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BVMinh.EmailService.Entity.Entities
{
    public class EmailRecipient : BaseEntity
    {
        [Required]
        public string EmailAddress { get; set; }
        public string RecipientName { get; set; }
        [BsonDictionaryOptions]
        public Dictionary<string, object> MergeData { get; set; }
        [BsonDictionaryOptions]
        public Dictionary<string, object> CustomArgs { get; set; }

    }
}
