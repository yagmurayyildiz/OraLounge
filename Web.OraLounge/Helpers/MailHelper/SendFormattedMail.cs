using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.OraLounge.Helpers.MailHelper
{
    public class SendFormattedMail
    {
        private string _sender;
        private readonly string _reciever;
        private readonly string _subject;
        public SendFormattedMail(string reciever, string subject)
        {
            _sender = ConfigurationManager.AppSettings["mailUser"];
            _reciever = reciever;
            _subject = subject;
        }
        public SendFormattedMail(string sender, string reciever, string subject)
        {
            _sender = sender;
            _reciever = reciever;
            _subject = subject;
        }

        public string Header { get; set; }
        public string Message { get; set; }

        MailSender mailSender = new MailSender()
        {
            Host = ConfigurationManager.AppSettings["mailHost"],
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["mailPort"]),
            Ssl = Convert.ToBoolean(ConfigurationManager.AppSettings["mailSsl"]),
            User = ConfigurationManager.AppSettings["mailUser"],
            Password = ConfigurationManager.AppSettings["mailPassword"]
        };

        public async Task SendTextMailAsync()
        {
            try
            {
                await mailSender.SendMailAsync(_sender, _reciever, _subject, Message, false);
            }
            catch (Exception)
            {

            }
        }

        public async Task SendHtmlFormattedMailAsync()
        {
            try
            {
                await mailSender.SendMailAsync(_sender, _reciever, _subject, PopulateBody(), true);
            }
            catch (Exception)
            {
            }
        }

        private string PopulateBody()
        {
            var body = new StringBuilder();
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Helpers/MailHelper/MailBody.html")))
            {
                body.Append(reader.ReadToEnd());
            }
            body.Replace("{_header}", Header)
                .Replace("{_message}", Message);
            return body.ToString();
        }
    }
}