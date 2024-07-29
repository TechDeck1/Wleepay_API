using Newtonsoft.Json;
using System.Web.Http;

namespace wleepay.API.Resources
{
    public class SochitelProductOperatorRequest
    {
        public SochitelAuth auth { get; set; }
        public string version { get; set; }
        public string command { get; set; }
       
        [JsonProperty("operator")]
        public string Operator { get; set; }
    }
}
