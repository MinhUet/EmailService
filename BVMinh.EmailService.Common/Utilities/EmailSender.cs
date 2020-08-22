using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BVMinh.EmailService.Common.Utilities
{
    public class EmailSender : IDisposable
    {
        public readonly SmtpClient SmtpClient;
        public EmailSender(string host, int port, string username, string password)
        {
            SmtpClient smtpClient = new SmtpClient(host);
            smtpClient.Port = port;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
            SmtpClient = smtpClient;
        }

        public void Dispose()
        {
            SmtpClient.Dispose();
        }
    }
}
