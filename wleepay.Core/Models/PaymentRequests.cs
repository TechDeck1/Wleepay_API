using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class PaymentRequests
    {
        public string CardNumber { get; set; }
        public string Expiration { get; set; } // Format: "MMYY"
        public string CVV { get; set; }
        public decimal Amount { get; set; }
        public string NameOnCard { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public string ReceiverPhoneNumber { get; set; }
        public int TransferTypeId { get; set; }

        //sochitel
        public RechargeRequestDetails RechargeData { get; set; }
  

    }

}
