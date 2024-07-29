using Newtonsoft.Json;

namespace wleepay.API.Resources
{
    public class ExecTransactionRequest
    {
        public SochitelAuth Auth { get; set; }
        public string Version { get; set; }
        public string Command { get; set; }
        public string Operator { get; set; }
        public string Msisdn { get; set; }
        public double Amount { get; set; }
        public string? Simulate { get; set; }
        public string? UserReference { get; set; }
        public string ProductId { get; set; }
        public long FromWalletId { get; set; }
        public string Currency { get; set; }
    }

    public class SochitelTransactionRequest
    {
        public SochitelAuth Auth { get; set; }
        public string version { get; set; }
        public string command { get; set; }
       
        [JsonProperty("operator")]
        public string Operator { get; set; }
        public string msisdn { get; set; }
        public double amount { get; set; }
        public string? simulate { get; set; }
        public string? userReference { get; set; }
        public string productId { get; set; }
    }

}
