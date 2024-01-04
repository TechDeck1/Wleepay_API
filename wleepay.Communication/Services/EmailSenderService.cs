using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wleepay.Communication.Interface;
using Twilio.TwiML.Messaging;
using wleepay.Communication.EmailTemplates;
using static wleepay.Communication.EmailTemplates.AccountDeactivatedTemplate;
using static wleepay.Communication.EmailTemplates.AccountActivatedTemplate;
using System.Numerics;
using System.Xml.Linq;
using static wleepay.Communication.EmailTemplates.PasswordChangeTemplate;
using static wleepay.Communication.EmailTemplates.EmailLockOutTemplate;

namespace wleepay.Communication.Services
{
    public class EmailSenderService : IEmailSender
    {
        public async Task<bool> SendEmailAsyncForLockAccountToSystemAdmin(string email, string name, string phone)
        {
            
                var subject = "Failed Login attempt";
                var recipientEmail = "shikha0dev@gmail.com";
                // var recipientEmail = email;

                EmailLockOutTemplateForSystemAdmin template = new EmailLockOutTemplateForSystemAdmin
                {
                    StaffEmail = email,
                    StaffFirstName = name,
                    StaffPhoneNumber = phone
                };

                string emailHtml = template.GenerateHtml();

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

                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

                smtpClient.Send(mailMessage);

                return true;

            
        }

        public async Task<bool> SendEmailAsyncForLockAccountToStaff(string email, string name, string phone)
        {

            var subject = "Failed Login attempt";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            EmailLockOutTemplateForStaff template = new EmailLockOutTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

            smtpClient.Send(mailMessage);

            return true;


        }

        public async Task<bool> SendForgotPasswordEmailLink(string email, string uri, string username)
        {
            var subject = "Reset password token ";
            var recipientEmail = "clarkflores524@gmail.com";

            string resetPasswordLink = uri;

            var body = $"Dear {username},\n\nPlease click the following link to reset your password:\n {resetPasswordLink}";

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

        public async Task<bool> SendPasswordChangeEmailToSystemAdminstrator(string email, string name , string phone)
        {

            var subject = "Alert: Wleepay portal password change notice (Do not reply)";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            PasswordChangeTemplateForSystemAdmin template = new PasswordChangeTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

            smtpClient.Send(mailMessage);

            return true;


        }

        public async Task<bool> SendPasswordChangeEmailToStaff(string email, string name, string phone)
        {

            var subject = "Alert: Wleepay portal password change notice (Do not reply)";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            PasswordChangeTemplateForStaff template = new PasswordChangeTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

            smtpClient.Send(mailMessage);

            return true;


        }

        public async Task<bool> SendEmailToStaffToCompleteLoginProcess(string email, string uri, string username)
        {

            var subject = "Complete Login process";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            string resetPasswordLink = uri;

            var body = $"Dear {username},\n\nPlease click the following link to complete your login process:\n {resetPasswordLink}";

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

        public async Task<bool> EmailForAccountActivatedToSystemAdmin(string email, string name, string phone)
        {
            var subject = "Wleepay portal account setup/activation confirmation notice (Do not reply)";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            AccountActivatedTemplateForSystemAdmin template = new AccountActivatedTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

            smtpClient.Send(mailMessage);

            return true;
        }

        public async Task<bool> EmailForAccountActivatedToStaff(string email, string name, string phone)
        {
            var subject = " Alert: Wleepay portal account setup/activation notice (Do not reply)";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            AccountActivatedTemplateForStaff template = new AccountActivatedTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

            smtpClient.Send(mailMessage);

            return true;
        }

        public async Task<bool> EmailForAccountDeactivatedToSystemAdmin(string email, string name, string phone )
        {
            var subject = " Alert: Wleepay portal account DeActivation notice (Do not reply)";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            AccountDeactivatedTemplateForSystemAdmin template = new AccountDeactivatedTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);

            smtpClient.Send(mailMessage);

            return true;
        }

        public async Task<bool> EmailForAccountDeactivatedToStaff(string email, string name, string phone)
        {
            var subject = " Alert: Wleepay portal account DeActivation notice (Do not reply)";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            AccountDeactivatedTemplateForStaff template = new AccountDeactivatedTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string emailHtml = template.GenerateHtml();

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

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, emailHtml);
            smtpClient.Send(mailMessage);
            return true;
        }

        public async Task<bool> EmailForAccountCreationToSystemAdmins(string emails, string userEmail)
        {
            var subject = "A New Account Created";
            var recipientEmail = "shikha0dev@gmail.com";
            // var recipientEmail = email;

            var body = $"Dear Admin,\n\n Account is succesfully created for {userEmail}.";

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

            //foreach (var recipientMail in emails)
            //{
            //    MailMessage mailMessage = new MailMessage(senderEmail, recipientMail, subject, body);
            //    mailMessage.IsBodyHtml = true; // You can set this to true if the body contains HTML
            //    smtpClient.Send(mailMessage);

            //}

            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);


            return true;
        }

    }
}
