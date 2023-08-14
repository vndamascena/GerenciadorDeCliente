﻿using System.Net;
using System.Net.Mail;
namespace ContasApp.Presentation.Helpers
{
    /// <summary>
    /// Classe para realizar envio de emails
    /// </summary>
    public class EmailMessageHelper
    {
        public static void SendMessage(string mailTo, string subject,
       string body)
        {
            #region Configurações da conta de email
            var conta = "gdc.senha@gmail.com";
            var senha = "fenomeno88";
            var smtp = "smtp.gmail.com";
            var porta = 587;
            #endregion
            var mailMessage = new MailMessage(conta, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            var smtpClient = new SmtpClient(smtp, porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(conta, senha);
            smtpClient.Send(mailMessage);
        }
    }
}