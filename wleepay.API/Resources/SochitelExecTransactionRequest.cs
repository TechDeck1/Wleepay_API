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
        public string AccountId { get; set; }

    }

    public class SochitelTransactionRequest
    {
        [JsonProperty("auth")]
        public SochitelAuth Auth { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("simulate")]
        public string Simulate { get; set; }

        [JsonProperty("userReference")]
        public string UserReference { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }
    }


}
