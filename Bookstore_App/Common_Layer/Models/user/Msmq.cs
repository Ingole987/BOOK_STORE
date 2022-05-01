using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common_Layer.Models.user
{
    public class Msmq
    {
        MessageQueue messageQueue = new MessageQueue();
        private string recivername;
        public void SendMessage(string token , string fullName)
        {
            messageQueue.Path = @".\private$\Tokens";
            try
            {
                recivername = fullName;
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("pratik987ingole@gmail.com", "Pratik@987")
                };
                mailMessage.From = new MailAddress("pratik987ingole@gmail.com");
                mailMessage.To.Add(new MailAddress("pratik987ingole@gmail.com"));
                string mailBody = $"<!DOCTYPE html>" +
                                  $"<html>" +
                                  $" <style>" +
                                  $".blink" +
                                  $"</style>" +
                                    $"<body style = \"background-color:#DBFF73;text-align:center;padding:5px;\">" +
                                    $"<h1 style = \"color:#6A8D02; border-bottom: 3px solid #84AF08; margin-top: 5px;\"> Dear <b>{recivername}</b> </h1>\n" +
                                    $"<h3 style = \"color:#8AB411;\"> For Resetting Password The Below Token Is Issued</h3>" +
                                    $"<h3 style = \"color:#8AB411;\"> Please Copy The Token And Paste It In Swagger Authorize Value</h3>" +
                                    $"<p style = \"color:#9DCF0C;\"> {token} </p>\n" +
                                    $"<h3 style = \"color:#8AB411;margin-bottom:5px;\"> <blink>This Token Will be Valid For Next 1 Hours<blink></h3>" +
                                    $"</body>" +
                                    $"</html>";

                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Fundoo Notes Password Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
