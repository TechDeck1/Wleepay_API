using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.API.Resources
{
    public class SochitelOperatorRequest
    {
        public SochitelAuth auth { get; set; }
        public string version { get; set; }
        public string command { get; set; }
        public string country { get; set; }
        public string productType { get; set; }
    }

}
