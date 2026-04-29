using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace wleepay.Core.DTO.Request
{
    public class MobileAirtimeRechargePayload
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
