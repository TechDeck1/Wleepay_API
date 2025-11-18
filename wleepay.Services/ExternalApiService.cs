using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class ExternalApiService: IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;
        private readonly string _apiKey;
        private readonly string _exchangeRateUrl;
        private readonly string _sochitelUrl;

        public ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = configuration["exchangerateApiKey"];
        }

        public async Task<decimal> GetConvertedCurrency(decimal amount)
        {
            string baseCurrency = "USD";
            string targetCurrency = "LRD";
            string url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/latest/{baseCurrency}";
            decimal convertedAmount = 0;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            ExchangeRateResponse rateResponse = JsonConvert.DeserializeObject<ExchangeRateResponse>(responseBody);

            if (rateResponse != null)
            {
                foreach (var rate in rateResponse.ConversionRates)
                {
                    if (rate.Key == targetCurrency)
                    {
                        convertedAmount = amount * rate.Value;
                        return convertedAmount;

                    }
                }
            }

            return convertedAmount;

        }

        public async Task<string> PostDataAsync<T>(T data)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://artx.sochitel.com/api.php");
                string json = JsonConvert.SerializeObject(data);
                //System.Text.Json.JsonSerializer.Serialize(data)
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var ffhg = await response.Content.ReadAsStringAsync();
                Console.WriteLine(ffhg);
                return ffhg;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }



        //public async Task<string> PostDataAsync<T>(T data)
        //{
        //    try
        //    {
        //        using var client = new HttpClient();
        //        var json = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            NullValueHandling = NullValueHandling.Ignore
        //        });

        //        Console.WriteLine("Sending JSON to Sochitel:\n" + json);

        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = await client.PostAsync("https://artx.sochitel.com/api.php", content);
        //        var result = await response.Content.ReadAsStringAsync();

        //        Console.WriteLine("Response:\n" + result);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("PostDataAsync error: " + ex.Message);
        //        throw;
        //    }
        //}





    }
}
