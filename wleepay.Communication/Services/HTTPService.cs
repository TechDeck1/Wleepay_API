using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using wleepay.Communication.Interface;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace wleepay.Communication.Services
{
    public class HTTPService : IHTTPService
    {
        private readonly IConfiguration _configuration;
        public HTTPService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> PostDataAsync(string url, string jsonObj)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpContent requestObj = new StringContent(jsonObj, UnicodeEncoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, requestObj);
                    var data = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
