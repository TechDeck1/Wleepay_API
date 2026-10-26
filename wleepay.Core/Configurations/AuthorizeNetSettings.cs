using wleepay.Core.Models;

namespace wleepay.Core.Configurations
{
         public class AuthorizeNetSettings
         {
            public string ApiLoginId { get; set; }
            public string TransactionKey { get; set; }
            public string Environment { get; set; }

        }

        public class SochitelSettings
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }


        public class CreatePaymentIntentRequest
        {
            public string Flow { get; set; }                  
            public decimal Amount { get; set; }              
            public string ReceiverPhoneNumber { get; set; }
            public int TransferTypeId { get; set; }
            public RechargeRequestDetails RechargeData { get; set; }
            public string AccountId { get; set; }            
            public decimal AmountOperator { get; set; }     
        }

        public class CreatePaymentIntentResponse
        {
            public string ClientSecret { get; set; }
            public string PaymentIntentId { get; set; }
            public decimal ChargeAmount { get; set; }         
        }

}
