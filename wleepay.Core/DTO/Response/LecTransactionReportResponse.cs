using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class LecTransactionReportResponse
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public int? Status { get; set; }
        public long WalletId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionType { get; set; }
        public string CurrencyCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal CreditAmountPurchased { get; set; }   // The credit amount purchased in the transaction
        public decimal WleeSendFeeCharged { get; set; }  // The WleeSend fee charged for the transaction
        public string ReceiverNumber { get; set; }       // Receiver's phone number or ID
        public string TransactionStatus { get; set; }

    }

}
