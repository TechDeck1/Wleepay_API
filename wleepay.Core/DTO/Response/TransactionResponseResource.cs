using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class TransactionResponseResource
    {
        public int TotalCount { get; set; }

        public List<Transaction> Transactions { get; set; }
    }

    public class SochitelTransactionResponseResource
    {
        public int TotalCount { get; set; }

        public List<SochitelTransactions> Transactions { get; set; }
    }
}
