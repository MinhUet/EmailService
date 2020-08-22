using BVMinh.EmailService.Entity.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Scheduler.EmailLogics
{
    public class EmailLogic
    {
        private readonly IConfiguration _configuration;
        private readonly int _maxRecipientsPerMail;
        public EmailLogic(IConfiguration configuration)
        {
            _configuration = configuration;
            _maxRecipientsPerMail = _configuration.GetSection("EmailPacket").GetValue<int>("MaxRecipientsPerMail");
        }

        public List<OutboxEmailDTO> DivisionEmail(OutboxEmailDTO email)
        {
            List<OutboxEmailDTO> EmailSendIntoKafka = new List<OutboxEmailDTO>();
            int indexEmail = 0;
            int looptimes = email.Recipients.Count;

            looptimes /= _maxRecipientsPerMail;
            InsertEmailIntoArray(email, ref EmailSendIntoKafka, _maxRecipientsPerMail, looptimes, indexEmail);
            return EmailSendIntoKafka;
        }

        private void InsertEmailIntoArray(
            OutboxEmailDTO email,
            ref List<OutboxEmailDTO> EmailSendIntoKafka,
            int amountEmailPerMessage,
            int looptimes,
            int indexEmail)
        {
            while (looptimes > 0)
            {
                OutboxEmailDTO sendEmail = (OutboxEmailDTO)email.Clone();
                sendEmail.Recipients = email.Recipients.GetRange(indexEmail, amountEmailPerMessage);
                indexEmail += amountEmailPerMessage;
                looptimes--;
                EmailSendIntoKafka.Add(sendEmail);
            }
            if (looptimes == 0)
            {
                OutboxEmailDTO sendEmail = (OutboxEmailDTO)email.Clone();
                sendEmail.Recipients = email.Recipients.GetRange(indexEmail, email.Recipients.Count - indexEmail);
                EmailSendIntoKafka.Add(sendEmail);
            }
        }
    }
}
