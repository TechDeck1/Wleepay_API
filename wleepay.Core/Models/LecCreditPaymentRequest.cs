using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace wleepay.Core.Models
{
  
    public class LecCreditPaymentRequest
    {
        public PaymentRequests Payment { get; set; }

        public decimal AmountOperator { get; set; } 
        public string AccountId { get; set; }
        public string OperatorId { get; set; }
        public object ExtraParameters { get; set; }
    }

    
}
