using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;

namespace wleepay.Core.Services
{
    public interface IExternalApiService
    {
        Task<string> PostDataAsync<T>(T data);
    }
}
