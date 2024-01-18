using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Types;
using wleepay.Communication.Interface;
using Twilio.Rest.Api.V2010.Account;

namespace wleepay.Communication.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IConfiguration _configuration;
        private readonly string _fromNumber;

        public WhatsAppService(IConfiguration configuration)
        {
            _configuration = configuration;
            string accountSid = configuration.GetSection("Twillio:SID").Value.ToString();
            string authToken = configuration.GetSection("Twillio:Token").Value.ToString();
            _fromNumber = configuration.GetSection("Twillio:FromNumber").Value.ToString();
            TwilioClient.Init(accountSid, authToken);
        }

        public string sendMessageForCustomerTicket(string message, string phoneNumber)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+917018519190"));
                messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                messageOptions.Body = $"Dear Customer" + " " + message;

                var messageResponse = MessageResource.Create(messageOptions);

                return messageResponse.Sid;
      
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }

        }
    }
}
