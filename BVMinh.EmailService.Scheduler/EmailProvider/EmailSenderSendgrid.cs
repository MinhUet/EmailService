using BVMinh.EmailService.Entity.DTO;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BVMinh.EmailService.Scheduler.EmailProvider
{
    public class EmailSenderSendgrid : BaseSender<OutboxEmailDTO>
    {
        private readonly SendGridClient _sendGridClient;

        public EmailSenderSendgrid(IConfiguration configuration,string username, string password) : base(configuration, username, password)
        {
            _sendGridClient = new SendGridClient(password);
        }

        public void CheckIsHtml(ref SendGridMessage sendGridMessage, string body, bool IsBodyHtml = false)
        {
            if (IsBodyHtml)
            {
                sendGridMessage.HtmlContent = body;
            }
            else
            {
                sendGridMessage.PlainTextContent = body;
            }
        }

        public override async Task<List<RecipientDTO>> SendMailMerge(OutboxEmailDTO outboxMail)
        {
            List<RecipientDTO> recipientDTOs = new List<RecipientDTO>();
            var from = new EmailAddress(outboxMail.EmailSenderAddress, outboxMail.EmailSenderName);
            var body = outboxMail.Body;

            foreach (var recipient in outboxMail.Recipients)
            {
                var msg = new SendGridMessage
                {
                    Subject = outboxMail.Subject,
                    From = from,
                    ReplyTo = new EmailAddress(outboxMail.EmailReplyToAddress ?? outboxMail.EmailSenderAddress)
                };
                body = MergeData(recipient.MergeData, outboxMail.Body);

                if (outboxMail.IsSendBulk)
                {
                    msg.CustomArgs = outboxMail.CustomArgs ?? new Dictionary<string, string>();
                }
                else
                {
                    msg.CustomArgs = recipient.CustomArgs ?? new Dictionary<string, string>();
                }
                msg.AddTo(recipient.RecipientAddress, recipient.RecipientName);
                CheckIsHtml(ref msg, body, outboxMail.IsBodyHtml);
                addCustomArgs(ref msg, outboxMail);
                try
                {
                    var res = await _sendGridClient.SendEmailAsync(msg);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK || res.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        Console.WriteLine("Done !" + res.StatusCode);
                    }
                    else
                    {
                        recipientDTOs.Add(recipient);
                    }
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
            var from = new EmailAddress(outboxMail.EmailSenderAddress, outboxMail.EmailSenderName);
            var body = outboxMail.Body;
            var msg = new SendGridMessage
            {
                Subject = outboxMail.Subject,
                From = from,
                ReplyTo = new EmailAddress(outboxMail.EmailReplyToAddress ?? outboxMail.EmailSenderAddress)
            };

            if (outboxMail.IsSendBulk)
            {
                msg.CustomArgs = outboxMail.CustomArgs ?? new Dictionary<string, string>();
                List<EmailAddress> addresses = new List<EmailAddress>();

                foreach (var recipient in outboxMail.Recipients)
                {
                    addresses.Add(new EmailAddress { Email = recipient.RecipientAddress, Name = recipient.RecipientName });
                }
                msg.AddTos(addresses);
                CheckIsHtml(ref msg, body, outboxMail.IsBodyHtml);
                addCustomArgs(ref msg, outboxMail);

                try
                {
                    var res = await _sendGridClient.SendEmailAsync(msg);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK || res.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        Console.WriteLine("Done !" + res.StatusCode);
                        return true;
                    }
                    return false;
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Send Fail ! error :" + ex);
                    return false;
                }
            }
            else
            {
                msg = new SendGridMessage
                {
                    Subject = outboxMail.Subject,
                    From = from,
                    ReplyTo = new EmailAddress(outboxMail.EmailReplyToAddress ?? outboxMail.EmailSenderAddress),
                    Personalizations = new List<Personalization>()
                };
                foreach (var recipient in outboxMail.Recipients)
                {
                    var personalization = new Personalization();
                    personalization.Tos = new List<EmailAddress> { new EmailAddress(recipient.RecipientAddress, recipient.RecipientName) };
                    personalization.CustomArgs = recipient.CustomArgs ?? new Dictionary<string, string>();
                    if (!personalization.CustomArgs.ContainsKey("ApplicationCode"))
                    {
                        personalization.CustomArgs.Add("ApplicationCode", outboxMail.ApplicationCode);
                    }
                    if (!personalization.CustomArgs.ContainsKey("SecretCode"))
                    {
                        personalization.CustomArgs.Add("SecretCode", SecretCode);
                    }
                    if (!personalization.CustomArgs.ContainsKey("CompanyCode"))
                    {
                        personalization.CustomArgs.Add("CompanyCode", outboxMail.CompanyCode);
                    }
                    if (!personalization.CustomArgs.ContainsKey("IsShare"))
                    {
                        personalization.CustomArgs.Add("IsShare", outboxMail.IsShare.ToString());
                    }
                    msg.Personalizations.Add(personalization);
                    msg.CustomArgs = recipient.CustomArgs ?? new Dictionary<string, string>();
                }
                CheckIsHtml(ref msg, body, outboxMail.IsBodyHtml);
                try
                {
                    var res = await _sendGridClient.SendEmailAsync(msg);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK || res.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        msg.CustomArgs = null;
                        Console.WriteLine("Done !" + res.StatusCode);
                        Console.WriteLine("");
                    }
                    Console.WriteLine(await res.Body.ReadAsStringAsync());
                    return true;
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Send Fail ! error :" + ex);

                    return false;
                }
            }

        }

        private void addCustomArgs(ref SendGridMessage msg, OutboxEmailDTO outboxMail)
        {
            if (!msg.CustomArgs.ContainsKey("SecretCode"))
            {
                msg.CustomArgs.Add("SecretCode", SecretCode);
            }
            if (!msg.CustomArgs.ContainsKey("ApplicationCode"))
            {
                msg.CustomArgs.Add("ApplicationCode", outboxMail.ApplicationCode);
            }
            if (!msg.CustomArgs.ContainsKey("CompanyCode"))
            {
                msg.CustomArgs.Add("CompanyCode", outboxMail.CompanyCode);
            }
            if (!msg.CustomArgs.ContainsKey("IsShare"))
            {
                msg.CustomArgs.Add("IsShare", outboxMail.IsShare.ToString());
            }
        }
    }
}
