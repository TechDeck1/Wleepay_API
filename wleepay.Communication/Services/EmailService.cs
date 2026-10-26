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
    public class EmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly string _senderEmail;
        private readonly string _senderName;


        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _senderEmail = configuration.GetSection("SendGrid:SendgridSenderEmail").Value.ToString();
            _senderName = configuration.GetSection("SendGrid:SendgridSenderName").Value.ToString();

        }

        //Change recipient email to "lionelbernard@wleepay.com" when deploy changes to live
        public async Task<string> EmailForPaymentRecieved(string messageText, string email)
        {
            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var emailSubject = "Payment Recieved";
            var recipientEmail = email;

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_senderEmail, _senderName),
                Subject = emailSubject,
                PlainTextContent = "",
                HtmlContent = messageText,
            };

            msg.AddTo(new EmailAddress(recipientEmail, "user"));
            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";
           
        }

        public async Task<string> SendEmailAsync(string email, string template, string name, string mailSubject)
        {

            var recipientEmail = email;
            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_senderEmail, _senderName),
                Subject = mailSubject,
                PlainTextContent = "",
                HtmlContent = template

            };
            msg.AddTo(new EmailAddress(recipientEmail, name));
            var response = await client.SendEmailAsync(msg);

            // A success status code means SendGrid received the email request and will process it.
            return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";

        }

        public async Task<string> SendEmailWithInlineImageAsync(
            string email, string template, string name, string mailSubject,
            string imageName, byte[] imageContent, string contentId)
        {
            if (imageContent == null || imageContent.Length == 0)
                throw new ArgumentException("Inline image content is required.", nameof(imageContent));
            if (string.IsNullOrWhiteSpace(contentId))
                throw new ArgumentException("Inline image content ID is required.", nameof(contentId));

            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_senderEmail, _senderName),
                Subject = mailSubject,
                PlainTextContent = "",
                HtmlContent = template
            };

            msg.AddTo(new EmailAddress(email, name));
            msg.AddAttachment(
                imageName,
                Convert.ToBase64String(imageContent),
                "image/png",
                "inline",
                contentId);

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";
        }

        public async Task<string> SendEmailToUserAsync(string userEmail, string subject, string htmlContent)
        {
            var apiKey = _configuration.GetSection("SendGrid:WleePaySendGridApiKey").Value.ToString();
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_senderEmail, _senderName),
                Subject = subject,
                PlainTextContent = "",
                HtmlContent = htmlContent
            };
            msg.AddTo(new EmailAddress(userEmail));
            var response = await client.SendEmailAsync(msg);
            var responseBody = await response.Body.ReadAsStringAsync(); 
            var status = response.StatusCode;
  
            return response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!";
        }

    }
}
