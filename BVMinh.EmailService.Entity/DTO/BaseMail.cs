using BVMinh.EmailService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.DTO
{
    public class BaseMail : BaseEntity
    {
        public string EmailID { get; set; }
        public string ApplicationCode { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Boolean IsBodyHtml { get; set; }
        public Boolean IsSendBulk { get; set; }
        public Boolean IsMerge { get; set; }
        public Boolean IsShare { get; set; }
        public Boolean IsSheduler { get; set; }
        public Dictionary<string, string> CustomArgs { get; set; }
        public string EmailSenderName { get; set; }
        public string EmailSenderAddress { get; set; }
        public List<RecipientDTO> Recipients { get; set; }
    }
}
