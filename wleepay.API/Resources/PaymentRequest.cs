using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class PaymentRequest
    {
        public string UserId { get; set; }
        public Core.Enums.PaymentRequestType Type { get; set; }
        public Core.Enums.TransactionStatus TransferStatus { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }

    public class AgentPaymentRequest
    {
        public Core.Enums.PaymentRequestType Type { get; set; }
        public Core.Enums.TransactionStatus TransferStatus { get; set; }
        public int TransferId {  get; set; }    
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }
}
