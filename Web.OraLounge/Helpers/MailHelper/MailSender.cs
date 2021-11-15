using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Web.OraLounge.Helpers.MailHelper
{
    public class MailSender
    {
        private string _host;
        private int _port;
        private bool _ssl;
        private string _user;
        private string _password;

        public string Host { get => _host; set => _host = value; }
        public int Port { get => _port; set => _port = value; }
        public bool Ssl { get => _ssl; set => _ssl = value; }
        public string User { get => _user; set => _user = value; }
        public string Password { get => _password; set => _password = value; }

        public Task SendMailAsync(string sender, string reciever, string subject, string body, bool isBodyHtml)
        {
            var smtp = new SmtpClient()
            {
                Host = _host,
                Port = _port,
                EnableSsl = _ssl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_user, _password)
            };

            var senderMail = new MailAddress(sender, sender);
            var recieverMail = new MailAddress(reciever, reciever);
            using (var message = new MailMessage(senderMail.Address, recieverMail.Address) { Subject = subject, Body = body, IsBodyHtml = isBodyHtml })
            {
                smtp.SendAsync(message, "");
            }
            return Task.FromResult(true);
        }
    }
}