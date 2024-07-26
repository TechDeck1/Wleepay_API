using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wleepay.Core.Services
{
    public interface IStripeService
    {
        public Task<string> CreatePaymentLinkAsync(double amount, string apiKey, string currency = "usd");

    }
}
