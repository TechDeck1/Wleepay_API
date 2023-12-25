using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wleepay.Communication.Interface;
using Twilio.TwiML.Messaging;

namespace wleepay.Communication.Services
{
    public class EmailSenderService : IEmailSender
    {
        public async Task<bool>  SendEmailAsyncForLockAccount(string email)
        {
            
                var subject = "Failed Login attempt";
                var recipientEmail = "shikha0dev@gmail.com";
               // var recipientEmail = email;

                var body = $"Dear Admin,\n\n Your staff's {email} account has been locked after 3 unsuccessful attempts.";

                // Sender's email address and credentials
                string senderEmail = "mvctest17@gmail.com";
                string senderPassword = "bqhyobxynxwbkteo"; // Use app-specific passwords or secure storage for passwords

                // Create SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Use your SMTP server

                // Port and SSL configuration
                smtpClient.Port = 587; // Port for SMTP (e.g., 587 for Gmail)
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

                smtpClient.Send(mailMessage);

                return true;

            
        }

        public async Task<bool> SendForgotPasswordEmailLink(string email, string uri, string username)
        {
            var subject = "Reset password token ";
            var recipientEmail = "clarkflores524@gmail.com";

            string resetPasswordLink = uri;

            var body = $"Dear {username},\n\nPlease click the following link to reset your password:\n {resetPasswordLink}";

            //string body = $@"
            //    <html>
            //    <body>
            //        <h1>Hello!</h1>
            //        <p>This is a sample email template with dynamic links:</p>
            //        <ul>
            //            <li><a href='{resetPasswordLink}'>Click Here</a></li>
            //        </ul>
            //        <p>Thank you!</p>
            //    </body>
            //    </html>
            //";

            // Sender's email address and credentials
            string senderEmail = "mvctest17@gmail.com";
            string senderPassword = "bqhyobxynxwbkteo"; // Use app-specific passwords or secure storage for passwords

            // Create SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Use your SMTP server

            // Port and SSL configuration
            smtpClient.Port = 587; // Port for SMTP (e.g., 587 for Gmail)
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

            smtpClient.Send(mailMessage);

            return true;
        }

        public async Task<bool> SendEmailToSystemAdminstrator(string email)
        {

            var subject = "Failed Login attempt";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            var body = $"Dear Admin,\n\n Your staff's {email} has changed his account password.";

            // Sender's email address and credentials
            string senderEmail = "mvctest17@gmail.com";
            string senderPassword = "bqhyobxynxwbkteo"; // Use app-specific passwords or secure storage for passwords

            // Create SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Use your SMTP server

            // Port and SSL configuration
            smtpClient.Port = 587; // Port for SMTP (e.g., 587 for Gmail)
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);

            smtpClient.Send(mailMessage);

            return true;


        }
    }
}
