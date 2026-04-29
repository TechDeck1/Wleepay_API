using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class TransactionHistory
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string TransferTypeName { get; set; }

        public int TransferTypeId { get; set; }

        public string OperatorId { get; set; }
        public string ProductId { get; set; }
        public string MeterNumber { get; set; }
    }
}
