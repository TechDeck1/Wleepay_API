using System;
using System.ComponentModel.DataAnnotations;
using wleepay.Core.DTO.Request;

namespace wleepay.API.Resources
{
    public class PaymentRequestForStripe
    {
        public SochitelAuth Auth { get; set; }
        public string Version { get; set; }
        public string Command { get; set; }
        public string Operator { get; set; }
        public string ProductId { get; set; }
        public string Msisdn { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long Amount { get; set; }
        public string Token { get; set; }
        public string? AccountId { get; set; }

        public ExtraParameters? ExtraParameters { get; set; }

    }

    public class MobileCheckoutPayload
    {
        [Required]
        public string ProductId { get; set; }

        public string StripeProductId { get; set; }
        public decimal Amount { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
        public string CouponCode { get; set; }
        public string PhoneNumber { get; set; }      // required for mobile recharge, optional for voucher
        public string Operator { get; set; }          // Sochitel operator id
        public string Command { get; set; }          // Sochitel command, same value your old RechargeData carried
        public int TransferTypeId { get; set; }
    }

    public class BillPaymentCheckoutPayload
    {
        [Required]
        public string MeterNumber { get; set; }
        public decimal Amount { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
        public string CouponCode { get; set; }
        [Required]
        public string ReceiverPhoneNumber { get; set; }
        public int OperatorId { get; set; }            
        public int ProductId { get; set; }       
        public string Command { get; set; }
        public int TransferTypeId { get; set; }
    }

}
