using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wleepay.Core.Services
{
    public interface IStripeService
    {
        public Task<string> CreatePaymentLinkAsync(decimal amount, string currency = "usd");

    }
}
