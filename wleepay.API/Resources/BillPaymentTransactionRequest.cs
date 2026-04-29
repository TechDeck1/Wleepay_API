using Newtonsoft.Json;
using wleepay.Core.DTO.Request;

namespace wleepay.API.Resources
{
    public class BillPaymentTransactionRequest
    {
        public SochitelAuth Auth { get; set; }
        public string Version { get; set; }
        public string Command { get; set; }
        public string Operator { get; set; }
        public string Msisdn { get; set; }
        public double AmountInUsd { get; set; }
        public string? Simulate { get; set; }
        public string? UserReference { get; set; }
        public string ProductId { get; set; }
        public long FromWalletId { get; set; }
        public int Currency { get; set; }
        public string AccountId { get; set; }
        public double? AmountInLd { get; set; }
        public ExtraParameters ExtraParameters { get; set; }

    }

    public class ExtraParameters
    {
        [JsonProperty("meterNumber")]
        public MeterNumber MeterNumber { get; set; }
    }

    public class MeterNumber
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SochitelBillPaymentTransactionRequest
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

        [JsonProperty("accountId")]
        public string AccountId { get; set; }


        [JsonProperty("amountOperator")]
        public string AmountOperator { get; set; }


        [JsonProperty("extraParameters")]
        public object extraParameters {  get; set; }

        [JsonProperty("extraParameter")]
        public ExtraParameters ExtraParameters { get; set; }



    }


}
