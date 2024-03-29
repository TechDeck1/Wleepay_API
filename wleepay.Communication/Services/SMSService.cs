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

        //client's to from number +12027318402 
        //client's From phone number +18564223507 
        //chnage both while deploying changes to live
        public string sendMessage(string message, string phoneNumber)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(
                new PhoneNumber(phoneNumber));
                messageOptions.From = new PhoneNumber("+18564223507");
                messageOptions.Body = message;

                var messageResponse = MessageResource.Create(messageOptions);
                var result = messageResponse.Status.ToString();
                return result;
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

