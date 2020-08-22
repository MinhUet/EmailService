using BVMinh.EmailService.Entity.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVMinh.EmailService.API.EmailLogics
{ 
    public class EmailLogic
    {
        private readonly IConfiguration _configuration;
        public readonly int _sumMaxRecipients;
        public readonly string _messageLimitRecipients;
        public readonly string _messageSendMailFail;
        public readonly string _schedulerTopic;
        public readonly string _shareTopic;
        public EmailLogic(IConfiguration configuration)
        {
            _configuration = configuration;
            _sumMaxRecipients = _configuration.GetSection("EmailPacket").GetValue<int>("SumMaxRecipients");
            _messageLimitRecipients = _configuration.GetSection("EmailPacket").GetValue<string>("MessageLimitRecipients");
            _messageSendMailFail = _configuration.GetSection("EmailPacket").GetValue<string>("MessageSendMailFail");
            _schedulerTopic = _configuration.GetSection("EmailPacket").GetValue<string>("SchedulerTopic");
            _shareTopic = _configuration.GetSection("EmailPacket").GetValue<string>("ShareTopic");
        }


        public string ChooseTopicName(OutboxEmailDTO emailDTO)
        {
            var nameTopic = emailDTO.ApplicationCode;
            if (emailDTO.IsSheduler == true)
            {
                nameTopic = _schedulerTopic;
            }
            else
            {
                if (emailDTO.IsShare == true)
                {
                    nameTopic = _shareTopic;
                }
            }
            return nameTopic;
        }
    }
}
