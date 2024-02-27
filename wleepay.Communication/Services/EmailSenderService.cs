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
using Newtonsoft.Json.Linq;
using static wleepay.Communication.EmailTemplates.EmailTemplates;

namespace wleepay.Communication.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;


        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Change recipient email to "lionelbernard@wleepay.com" when deploy changes to live
        public async Task<bool> EmailForAccountActivatedToStaff(string email, string name, string phone)
        {
            var subject = " Alert: Wleepay portal account setup/activation notice (Do not reply)";
              var recipientEmail = email;
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

        public async Task<bool> ReplyForCustomerTicket(string message, string email, string subject)
        {
            var emailSubject = subject;
              var recipientEmail = email;

            string htmlTemplate = message;

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

        public async Task<string> EmailForPaymentRecieved(string messageText, string email)
        {
            var emailSubject = "Payment Recieved";
              var recipientEmail = email;

            string htmlTemplate = messageText;

            // Sender'email;als
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = emailSubject,
                PlainTextContent = "Dear..",
                HtmlContent = htmlTemplate,
            };

            //msg.AddCc(email);

            msg.AddTo(new EmailAddress(recipientEmail, "shikha"));

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.Body.ToString(), response.StatusCode);

            string responseBody = await response.Body.ReadAsStringAsync();

            // A success status code means SendGrid received the email request and will process it.
            // Errors can still occur when SendGrid tries to send the email. 
            // If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 

            Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
            return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";
           
        }

        public async Task<string> SendEmailAsync(string email, string template, string name, string mailSubject)
        {

            var subject = mailSubject;
            // recipient's email address and credentials
            var recipientEmail = email;

            // Sender's email address and credentials
            string senderEmail = "ranjotk@arielsoftwares.com";

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "lionel"),
                Subject = subject,
                PlainTextContent = "",
                HtmlContent = template

            };
            msg.AddTo(new EmailAddress(recipientEmail, name));
            var response = await client.SendEmailAsync(msg);

            // A success status code means SendGrid received the email request and will process it.
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
                return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";
            }
            else
            {
                return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";

            }

        }

    }
}
