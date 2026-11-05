using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class LecBillRechargePayload
    {
        [JsonProperty("auth")]
        public SochitelAuth Auth { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("amountOperator")]
        public string AmountOperator { get; set; }

        [JsonProperty("simulate")]
        public bool Simulate { get; set; }

        //[JsonProperty("productId")]
        //public string ProductId { get; set; }

        //[JsonProperty("userReference")]
        //public string UserReference { get; set; }

        [JsonProperty("extraParameters")]
        public object ExtraParameters { get; set; } = new { accountType = "OFFLINE_PREPAID" };
    }

    public class VerifyLecMeterRequest
    {
        [Required]
        public string MeterNumber { get; set; }

        [Required]
        public string Operator { get; set; }
    }

    public class VerifyLecMeterResponse
    {
        public bool IsValid { get; set; }
        public bool ProviderError { get; set; }   
        public string Message { get; set; }
        public string Details { get; set; }    
    }



}
