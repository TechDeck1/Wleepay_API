using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class CreateTransactionResource
    {
        public long Id { get; set; }
        public long ToWalletId { get; set; }
        public long FromWalletId { get; set; }
        public float Amount { get; set; }
        public int TransferTypeId { get; set; }
        public long? ParentId { get; set; }
        public string Description { get; set; }
    }
}
