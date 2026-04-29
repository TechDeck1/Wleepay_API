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
        //public SMSService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    string accountSid = configuration.GetSection("Twillio:SID").Value.ToString();
        //    string authToken = configuration.GetSection("Twillio:Token").Value.ToString();
        //    _fromNumber = configuration.GetSection("Twillio:FromNumber").Value.ToString();
        //    TwilioClient.Init(accountSid, authToken);
        //}


        public SMSService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // Safely read Twilio configuration values
            string accountSid = _configuration["Twilio:SID"];
            string authToken = _configuration["Twilio:Token"];
            _fromNumber = _configuration["Twilio:FromNumber"];

            // Validate configuration
            if (string.IsNullOrWhiteSpace(accountSid))
                throw new InvalidOperationException("Configuration missing: Twilio:SID");

            if (string.IsNullOrWhiteSpace(authToken))
                throw new InvalidOperationException("Configuration missing: Twilio:Token");

            if (string.IsNullOrWhiteSpace(_fromNumber))
                throw new InvalidOperationException("Configuration missing: Twilio:FromNumber");

            // Initialize Twilio client
            TwilioClient.Init(accountSid, authToken);
        }

        public string sendMessage(string message, string phoneNumber)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(
                new PhoneNumber(phoneNumber));
                messageOptions.From = new PhoneNumber("+18564223507");
                messageOptions.Body = message;

                var messageResponse = MessageResource.Create(messageOptions);
                if(messageResponse.Status.ToString() != "200")
                {
                    var result = messageResponse.Status.ToString();
                    return "success";
                }
                else
                {
                    return "error";
                }
               
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #region CommentedCode
        //public string sendSmsForCustomerTicket(string message, string phoneNumber)
        //{
        //    try
        //    {
        //        var messageOptions = new CreateMessageOptions(
        //        new PhoneNumber("+917018519190"));
        //        messageOptions.From = new PhoneNumber("+18312194108");
        //        messageOptions.Body = message;


        //        var messageResponse = MessageResource.Create(messageOptions);
        //        var result = messageResponse.Status.ToString();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message.ToString();
        //    }
        //}

        //public string sendSmsForAccountDeactivation(string message, string phoneNumber)
        //{
        //    try
        //    {
        //        var messageOptions = new CreateMessageOptions(
        //        new PhoneNumber("+917018519190"));
        //        messageOptions.From = new PhoneNumber("+18312194108");
        //        messageOptions.Body = message;


        //        var messageResponse = MessageResource.Create(messageOptions);
        //        var result = messageResponse.Status.ToString();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message.ToString();
        //    }

        #endregion
    }
}

