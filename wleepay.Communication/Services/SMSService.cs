using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using wleepay.Communication.Interface;
using Microsoft.Extensions.Configuration;

namespace wleepay.Communication.Services
{
    public class SMSService : ISMSService
    {
        private readonly IConfiguration _configuration;
        private readonly string _fromNumber;
        public SMSService(IConfiguration configuration)
        {
            _configuration = configuration;
            string accountSid = configuration.GetSection("Twillio:SID").Value.ToString();
            string authToken = configuration.GetSection("Twillio:Token").Value.ToString();
            _fromNumber = configuration.GetSection("Twillio:FromNumber").Value.ToString();
            TwilioClient.Init(accountSid, authToken);
        }
        public string sendMessage(string message, string phoneNumber)
        {
            try
            {
                //PhoneNumber to = new PhoneNumber($"{phoneNumber}");
                //MessageResource messageRes = MessageResource.Create(
                //    to,
                //    from: new PhoneNumber(_fromNumber), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).  
                //    body: message);

                //return messageRes.Status.ToString();
                var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+919878192599"));
                messageOptions.From = new PhoneNumber("+18168399633");
                messageOptions.Body = message;


                var messageResponse = MessageResource.Create(messageOptions);
                Console.WriteLine(messageResponse.Body);
                return messageResponse.Status.ToString();
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
