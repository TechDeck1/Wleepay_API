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
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Twilio;

namespace wleepay.Communication.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly string _fromNumber;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
            string sendGridApiKey = configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
        }

        public async Task<bool> SendEmailAsyncForLockAccountToSystemAdmin(string email, string name, string phone)
        {
            
            var subject = "Failed Login attempt";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            EmailLockOutTemplateForSystemAdmin template = new EmailLockOutTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
            
        }

        public async Task<bool> SendEmailAsyncForLockAccountToStaff(string email, string name, string phone)
        {

            var subject = "Failed Login attempt";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            EmailLockOutTemplateForStaff template = new EmailLockOutTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }


        }

        public async Task<bool> SendForgotPasswordEmailLink(string email, string uri, string username)
        {
            var subject = "Reset password token ";
            var recipientEmail = "ariel.ranjot@gmail.com";

            string resetPasswordLink = uri;

            var htmlTemplate = $"Dear {username},\n\nPlease click the following link to reset your password:\n {resetPasswordLink}";

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> SendPasswordChangeEmailToSystemAdminstrator(string email, string name , string phone)
        {

            var subject = "Alert: Wleepay portal password change notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            PasswordChangeTemplateForSystemAdmin template = new PasswordChangeTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }


        }

        public async Task<bool> SendPasswordChangeEmailToStaff(string email, string name, string phone)
        {

            var subject = "Alert: Wleepay portal password change notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            PasswordChangeTemplateForStaff template = new PasswordChangeTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }


        }

        public async Task<bool> SendEmailToStaffToCompleteLoginProcess(string email, string uri, string username)
        {

            var subject = "Complete Login process";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            string resetPasswordLink = uri;

            var htmlTemplate = $"Dear {username},\n\nPlease click the following link to complete your login process:\n {resetPasswordLink}";

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }


        }

        public async Task<bool> EmailForAccountActivatedToSystemAdmin(string email, string name, string phone)
        {
            var subject = "Wleepay portal account setup/activation confirmation notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            AccountActivatedTemplateForSystemAdmin template = new AccountActivatedTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> EmailForAccountActivatedToStaff(string email, string name, string phone)
        {
            var subject = " Alert: Wleepay portal account setup/activation notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            AccountActivatedTemplateForStaff template = new AccountActivatedTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> EmailForAccountDeactivatedToSystemAdmin(string email, string name, string phone )
        {
            var subject = " Alert: Wleepay portal account DeActivation notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            AccountDeactivatedTemplateForSystemAdmin template = new AccountDeactivatedTemplateForSystemAdmin
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> EmailForAccountDeactivatedToStaff(string email, string name, string phone)
        {
            var subject = " Alert: Wleepay portal account DeActivation notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            AccountDeactivatedTemplateForStaff template = new AccountDeactivatedTemplateForStaff
            {
                StaffEmail = email,
                StaffFirstName = name,
                StaffPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> EmailForAccountCreationToSystemAdmins(string emails, string userEmail)
        {
            var subject = "A New Account Created";
            var recipientEmail = "ariel.ranjot@gmail.com";
            string htmlTemplate = $"Dear Admin,\n\n Account is succesfully created for {userEmail}.";

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";
            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);

            //foreach (var recipientMail in emails)
            //{
            //    MailMessage mailMessage = new MailMessage(senderEmail, recipientMail, subject, body);
            //    mailMessage.IsBodyHtml = true; // You can set this to true if the body contains HTML
            //    smtpClient.Send(mailMessage);
            //}

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);

            // A success status code means SendGrid received the email request and will process it.
            // Errors can still occur when SendGrid tries to send the email. 
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> ReplyForCustomerTicket(string Message, string email, string subject)
        {
            var emailSubject = subject;
            var recipientEmail = "ariel.ranjot@gmail.com";

            string htmlTemplate = Message;

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // Errors can still occur when SendGrid tries to send the email. 
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
        }

        public async Task<bool> SendEmailToSystemAdminToCompleteSignUp(string email, string uri, string username)
        {
            var subject = "Complete sign up process";
            var recipientEmail = "ariel.ranjot@gmail.com";
            string resetPasswordLink = uri;

            var htmlTemplate = $"Dear {username},\n\nPlease click the following link to complete your sign up process:\n {resetPasswordLink}";

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }

        }

        public async Task<bool> EmailForAccountCreationToSystemAdmin(string email, string name, string phone)
        {
            var subject = "Wleepay portal account setup/activation confirmation notice (Do not reply)";
            var recipientEmail = "ariel.ranjot@gmail.com";
            // var recipientEmail = email;

            AccountCreationTemplateForSystemAdmin template = new AccountCreationTemplateForSystemAdmin
            {
                SystemAdminEmail = email,
                SystemAdminFirstName = name,
                SystemAdminPhoneNumber = phone
            };

            string htmlTemplate = template.GenerateHtml();

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate

            };
            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            // A success status code means SendGrid received the email request and will process it.
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return true;
            }
            else
            {
                return false;

            }
            
        }
    }
}
