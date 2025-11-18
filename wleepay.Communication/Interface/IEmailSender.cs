using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace wleepay.Communication.Interface
{
    public interface IEmailSender
    {
        Task<string> EmailForPaymentRecieved(string messageText, string email);
        Task<string> SendEmailAsync(string email,string template, string username, string mailSubject);

        Task<string> SendEmailToUserAsync(string userEmail, string subject, string htmlContent);  // added 

    }
}
