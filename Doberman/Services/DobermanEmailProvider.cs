using System;
using System.Net.Mail;

namespace Doberman.Services
{
    public class DobermanEmailProvider : IEmailProvider
    {
        public void Send(string from, string to, string subject, string body)
        {
            // TODO: Try and test this.
            // http://haacked.com/archive/2006/05/30/ATestingMailServerForUnitTestingEmailFunctionality.aspx

            MailMessage email = new MailMessage(from, to);
            email.Subject = subject;
            email.Body = body;
            email.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Send(email);
        }
    }
}
