using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wleepay.Communication.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;


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
        public async Task<string> EmailForPaymentRecieved(string messageText, string email)
        {
            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var emailSubject = "Payment Recieved";
            var recipientEmail = email;
            string htmlTemplate = messageText;

            // Sender's email
            string senderEmail = "ranjotk@arielsoftwares.com";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(senderEmail, "WleePay Team"),
                Subject = emailSubject,
                PlainTextContent = "",
                HtmlContent = htmlTemplate,
            };

            msg.AddTo(new EmailAddress(recipientEmail, "user"));
            var response = await client.SendEmailAsync(msg);
            string responseBody = await response.Body.ReadAsStringAsync();

            // A success status code means SendGrid received the email request and will process it.
            // Errors can still occur when SendGrid tries to send the email. 
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
                From = new EmailAddress(senderEmail, "WleePay Team"),
                Subject = subject,
                PlainTextContent = "",
                HtmlContent = template

            };
            msg.AddTo(new EmailAddress(recipientEmail, name));
            var response = await client.SendEmailAsync(msg);

            // A success status code means SendGrid received the email request and will process it.
            return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";

        }

    }
}
