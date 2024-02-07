using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class PagingRequest
    {
        public String Email { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public Core.Enums.Role Role { get; set; }
    }

    public class Pagination
    {
        public string Email { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class FilterLogs 
    {
        public string Email { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class SystemLogsPagination
    {
        public string UserId { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class TransactionPagingRequest
    {
        public int TransferTypeId { get; set; }
        public int CurrencyTypeId { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class CashOnHandRequest 
    {
        public int WalletTypeId { get; set; }
        public int CurrencyTypeId { get; set; }
        public double Amount { get; set; }
    }

    public class PagingRequestForRates
    {
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class PagingRequestForCashFlow
    {
        public int CurrencyId { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
    }


}
