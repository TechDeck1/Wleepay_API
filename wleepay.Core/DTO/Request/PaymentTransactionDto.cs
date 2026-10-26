using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class PaymentTransactionDto
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ReceiverPhoneNumber { get; set; }
        public int TransferTypeId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        //public string? Country { get; set; }
        public string UserId { get; set; }
        public int CurrencyId { get; set; }
        public string ErrorMessage { get; set; }
    }

}
