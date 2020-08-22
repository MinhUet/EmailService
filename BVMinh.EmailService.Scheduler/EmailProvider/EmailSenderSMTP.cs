using BVMinh.EmailService.Entity.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BVMinh.EmailService.Scheduler.EmailProvider
{
    public class EmailSenderSMTP : BaseSender<OutboxEmailDTO>
    {
        public SmtpClient _smtpClient;
        public EmailSenderSMTP(
            IConfiguration configuration,
            string username,
            string password) : base(configuration, username, password)
        {
            _configuration = configuration;
            SmtpClient smtpClient = new SmtpClient(_configuration.GetSection("SMTPGmail:Host").Value, Convert.ToInt32(_configuration.GetSection("SMTPGmail:Port").Value));
            smtpClient.EnableSsl = Convert.ToBoolean(_configuration.GetSection("SMTPGmail:EnableSsl").Value);
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            _smtpClient = smtpClient;
        }


        public void MergeObject(Dictionary<String, string> _datas, ref MailMessage mailMessage)
        {
            foreach (var data in _datas)
            {
                mailMessage.Headers.Add(data.Key, data.Value);
            }
        }
        
        public override async Task<List<RecipientDTO>> SendMailMerge(OutboxEmailDTO outboxMail)
        {
            List<RecipientDTO> recipientDTOs = new List<RecipientDTO>();
            MailMessage mailMessage = new MailMessage();
            mailMessage.Sender = new MailAddress(outboxMail.EmailSenderAddress, outboxMail.EmailSenderName);
            mailMessage.From = new MailAddress(outboxMail.EmailSenderAddress, outboxMail.EmailSenderName);
            mailMessage.Subject = outboxMail.Subject;
            mailMessage.IsBodyHtml = outboxMail.IsBodyHtml;
            mailMessage.Body = outboxMail.Body;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;

            foreach (var recipient in outboxMail.Recipients)
            {
                mailMessage.Body = MergeData(recipient.MergeData, outboxMail.Body);
                if (outboxMail.IsSendBulk)
                {
                    MergeObject(outboxMail.CustomArgs, ref mailMessage);
                }
                else
                {
                    MergeObject(recipient.CustomArgs, ref mailMessage);
                }
                mailMessage.To.Add(new MailAddress(recipient.RecipientAddress, recipient.RecipientName));
                try
                {
                    _smtpClient.Send(mailMessage);
                    mailMessage.To.Clear();
                    Console.WriteLine("Done !");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Send Fail ! " + recipient.RecipientAddress + ":" + ex);
                    if (!recipientDTOs.Contains(recipient))
                    {
                        recipientDTOs.Add(recipient);
                    }
                }
            }
            return recipientDTOs;
        }

        public async override Task<bool> SendMailNoMerge(OutboxEmailDTO outboxMail)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.Sender = new MailAddress(outboxMail.EmailSenderAddress, outboxMail.EmailSenderName);
            mailMessage.From = new MailAddress(outboxMail.EmailSenderAddress, outboxMail.EmailSenderName);
            mailMessage.Subject = outboxMail.Subject;
            mailMessage.IsBodyHtml = outboxMail.IsBodyHtml;
            mailMessage.Body = outboxMail.Body;
            mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            if (outboxMail.IsSendBulk)
            {
                MergeObject(outboxMail.CustomArgs, ref mailMessage);
                foreach (var recipient in outboxMail.Recipients)
                {
                    mailMessage.To.Add(new MailAddress(recipient.RecipientAddress, recipient.RecipientName));
                }
                try
                {
                    _smtpClient.Send(mailMessage);
                    Console.WriteLine("Done !");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Send Fail ! error :" + ex);
                    return false;
                }
            }
            else
            {
                foreach (var recipient in outboxMail.Recipients)
                {
                    MergeObject(outboxMail.CustomArgs, ref mailMessage);
                    mailMessage.To.Add(new MailAddress(recipient.RecipientAddress, recipient.RecipientName));
                    try
                    {
                        _smtpClient.Send(mailMessage);
                        mailMessage.To.Clear();
                        Console.WriteLine("Done !");
                    }
                    catch (SmtpException ex)
                    {
                        Console.WriteLine("Send Fail ! error :" + ex);

                        return false;
                    }
                }
            }

            return true;
        }

    }
}
