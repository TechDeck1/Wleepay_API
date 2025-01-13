using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class WalletAmountHistoryByMonths
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public double TotalCount { get; set; }
    }

    public class WalletsBeginingBalance 
    {
        public double BeginningBalance { get; set; }
    }

    public class WalletsEndingBalance
    {
        public double EndingBalance { get; set; }
    }

    public class CreditsByWleepay
    {
        public double LDCreditToAgent { get; set; }
        public double LDCreditToMember { get; set; }
        public double USDCreditToAgent { get; set; }
        public double USDCreditToMember { get; set; }
    }

    public class DebitsByWleepay
    {
        public double LDDebitsFromAgent { get; set; }
        public double LDDebitFromMember { get; set; }
        public double USDDebitsFromAgent { get; set; }
        public double USDDebitsFromMember { get; set; }
    }

    public class GetWalletBalanceResponse
    {
        public int WalletType { get; set; }
        public DateTime DateTime { get; set; }

        public int CurrencyId { get; set; }

    }

    public class GetBusinessReport
    {
        public string? Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CurrencyType { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class GetCashOnHandByWleepay
    {
        public int WalletType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CurrencyType { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class CashOnHandReponse
    {
        public int total { get; set; }
        public List<Transaction> transacion { get; set; }

    }

    public class GetBillPaymentCommissionEarnedResponse
    {
        public int Total { get; set; }
        public List<SochitelTransactions> billCommissionEarned;

    }

    public class GetCashOutCommissionEarnedResponse
    {
        public int Total { get; set; }
        public List<Transaction> cashOutCommissionEarned;
    }

    public class GetFloatAccount
    {
        public int WalletType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CurrencyType { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class GetFloatAccountResponse
    {
        public int Total { get; set; }
        public List<Transaction> Transaction { get; set; }

    }

    public class GetReconciliationReport
    {
        public string userId { get; set; }
        public DateTime DateTime { get; set; }
        public int CurrencyType { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class GetReconciliationResponse
    {
        public int Total { get; set; }
        public List<Wallet> reconciliations;
    }
    public class GetReconciliationMonthlyBalance
    {
        public string Month { get; set; } 
        public string WalletName { get; set; }
        public double Balance { get; set; } 
        public string UserEmail { get; set; }
    }
}
