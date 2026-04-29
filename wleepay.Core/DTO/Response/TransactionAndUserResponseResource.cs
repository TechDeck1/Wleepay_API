using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.DTO.Response
{
    public class TransactionAndUserResponseResource
    {
        public ApplicationUser UserDetails { get; set; }

        public IList<Transaction> Transactions { get; set; }

    }
}
