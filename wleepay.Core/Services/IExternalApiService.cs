using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wleepay.Core.Services
{
    public interface IExternalApiService
    {
        Task<string> PostDataAsync(object data);

    }
}
