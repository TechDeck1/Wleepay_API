using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.Models;


namespace wleepay.Core.Services
{
    public interface IAuthorizeNetService
    {
        Task<PaymentTransaction> AuthorizePaymentAsync(PaymentRequests request, string userId);
        Task<PaymentTransaction> CapturePaymentAsync(string transactionId);
        Task<PaymentTransaction> VoidPaymentAsync(string transactionId);
        Task<VerifyLecMeterResponse> VerifyLecMeterAsync(string meterNumber, string operatorId);
        Task<PaymentTransaction> ProcessMobileRechargeAsync(PaymentRequests request, string userId);
        Task<PaymentTransaction> ProcessLecRechargeAsync(LecCreditPaymentRequest request, string userId);
    }
}
