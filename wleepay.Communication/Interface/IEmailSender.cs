using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace wleepay.Communication.Interface
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email);
        Task<bool> SendForgotPasswordEmailLink(string email, string uri);

    }
}
