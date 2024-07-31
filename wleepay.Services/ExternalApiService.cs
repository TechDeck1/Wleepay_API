using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<decimal> GetConvertedCurrency(string apiKey, string baseCurrency, string targetCurrency, decimal amount)
        {
            string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}";
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
                var request = new HttpRequestMessage(HttpMethod.Get, "https://artx.sochitel.com/api.php");
                string json = JsonConvert.SerializeObject(data);
                //System.Text.Json.JsonSerializer.Serialize(data)
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var ffhg= await response.Content.ReadAsStringAsync();
                Console.WriteLine(ffhg);
                return ffhg;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }

    }
}
