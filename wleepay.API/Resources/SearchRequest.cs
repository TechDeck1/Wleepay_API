using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class SearchRequest
    {
        public string LoginName { get; set; }
        public string PhoneNumber { get; set; }
        public Core.Enums.Role? Role { get; set; }
    }

    public class SearchRequestByEmail
    {
        public string Email { get; set; }
        public Core.Enums.Role Role { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class SearchRequestForVerification
    {
        public string PhoneNumber { get; set; }
    }

    public class ReportByUserStatusRequest
    {
        public int Role { get; set; }
        public int Status { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }


    public class ReportByWalletBalanceRequest
    {
        public int Role { get; set; }
        public int CurrencyType { get; set; }
        public int Balance { get; set; }
        public int WalletType { get; set; }

    }

    public class TransactionCountSummaryRequest
    {
        public int Role { get; set; }
        public int TransferType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

    }

    public class SochitelTransactionCommisionSummaryRequest
    {
        public int Role { get; set; }
        public string TransactionType { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

    }
}
