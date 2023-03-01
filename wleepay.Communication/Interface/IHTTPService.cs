using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wleepay.Communication.Interface
{
    public interface IHTTPService
    {
        Task<string> PostDataAsync(string url, string jsonObj);
    }
}
