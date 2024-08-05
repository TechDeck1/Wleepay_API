using Newtonsoft.Json;
using System.Collections.Generic;
using wleepay.API.Helpers;
using wleepay.Core.Common;

namespace wleepay.API.Resources
{
    public class SochitelTransactionResponse
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonConverter(typeof(CustomResultJsonConverter))]
        public Result Result { get; set; }

    }

    public class Amount
    {
        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }

    public class Balance
    {
        [JsonProperty("initial")]
        public string Initial { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("commission")]
        public int Commission { get; set; }

        [JsonProperty("commissionPercentage")]
        public int CommissionPercentage { get; set; }

        [JsonProperty("final")]
        public string Final { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("operator")]
        public Operator Operator { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("amount")]
        public Amount Amount { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("userReference")]
        public string UserReference { get; set; }

        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty("simulation")]
        public string? Simulation { get; set; }

        [JsonProperty("accountId")]
        public string? AccountId { get; set; }

        [JsonProperty("balance")]
        public Balance Balance { get; set; }
    }



}
