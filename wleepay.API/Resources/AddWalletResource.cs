using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class AddWalletResource
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public int CurrencyId { get; set; }
        public int Type { get; set; }
        public float Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
