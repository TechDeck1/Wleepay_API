using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class TaxRateResourceModel
    {
        public int Id { get; set; }
        public double TaxAmount { get; set; }
        public double OrganizationFee { get; set; }
        public double AgentCommission { get; set; }
        public string Description { get; set; }

    }
}
