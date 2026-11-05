using System;
using System.Collections.Generic;

namespace wleepay.Core.DTO.Response
{
    public class MobileDraftDto
    {
        public string Kind { get; set; } = "";
        public string? OperatorId { get; set; }
        public string? ProductId { get; set; }
        public string? ReceiverPhone { get; set; }
        public string? MeterNumber { get; set; }
        public decimal Amount { get; set; }          // base amount, before fee
    }

    public class MobileTransactionDto
    {
        public string Id { get; set; } = "";
        public string Kind { get; set; } = "";
        public string Product { get; set; } = "";
        public string Provider { get; set; } = "";
        public string Receiver { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public decimal Fee { get; set; }
        public MobileDraftDto Draft { get; set; } = new MobileDraftDto();
    }

    public class MobileTransactionPage
    {
        public int TotalCount { get; set; }
        public Dictionary<string, decimal> Totals { get; set; } = new Dictionary<string, decimal>();
        public List<MobileTransactionDto> Data { get; set; } = new List<MobileTransactionDto>();
    }
}
