using BVMinh.EmailService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.DTO
{
    [Serializable]
    public class OutboxEmailDTO : BaseEntity, ICloneable
    {
        public string EmailID { get; set; }
        public string ApplicationCode { get; set; }
        public string CompanyCode { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Boolean IsBodyHtml { get; set; }
        public Boolean IsSheduler { get; set; }
        public Boolean IsSendBulk { get; set; }
        public Boolean IsShare { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string EmailReplyToAddress { get; set; }
        public Dictionary<string, string> CustomArgs { get; set; }
        public string EmailSenderName { get; set; }
        public string EmailSenderAddress { get; set; }
        public string ShareTopicName { get; set; }
        public Boolean IsMerge { get; set; }
        public int RetryTime { get; set; }
        public List<RecipientDTO> Recipients { get; set; }
        public object Clone()
        {
            return (OutboxEmailDTO)this.MemberwiseClone();
        }
    }
}
