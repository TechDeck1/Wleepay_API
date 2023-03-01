using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class AgentSearchRequest
    {
        public string Lat { get; set; }
        public string Long { get; set; }
        public int? Distance { get; set; }
    }
}
