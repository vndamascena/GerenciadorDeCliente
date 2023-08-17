using System;
using System.Net;
using System.Net.Mail;

namespace ContasApp.Presentation.Helpers
{
    public class EmailMessageHelper
    {
        public static void SendMessage(string mailTo, string subject, string body)
        {
            try
            {
                var conta = "gdc.senha@outlook.com";
                var senha = "fenomeno88";
                var smtp = "smtp-mail.outlook.com";
                var porta = 587;

                using (var mailMessage = new MailMessage(conta, mailTo))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient(smtp, porta))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(conta, senha);
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception e)
            {
                

            }
        }
    }
}
