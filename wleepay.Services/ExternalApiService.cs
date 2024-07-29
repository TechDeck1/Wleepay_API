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

        //public async Task<string> PostDataAsync(object data)
        //{
        //    try
        //    {
        //        //var jsonContent = System.Text.Json.JsonSerializer.Serialize(data);
        //        var jsonContent = JsonConvert.SerializeObject(data);
        //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        //        var response = await _httpClient.PostAsync("https://artx.sochitel.com/api.php", content);

        //        response.EnsureSuccessStatusCode();

        //        return await response.Content.ReadAsStringAsync();
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError($"Request error: {ex.Message}");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Unexpected error: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task<string> PostDataAsync<T>(T data)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://artx.sochitel.com/api.php");
                string json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var ffhg= await response.Content.ReadAsStringAsync();
                Console.WriteLine(ffhg);

                return ffhg;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }

    }
}
