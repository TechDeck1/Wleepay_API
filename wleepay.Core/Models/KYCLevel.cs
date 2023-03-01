using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class KYCLevel
    {
        public KYCLevel()
        {
            UserDetails = new Collection<UserDetail>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double? TransactionDayLimit { get; set; }
        public double? TransactionMonthLimit { get; set; }
        public double? AccountBalanceLimit { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
