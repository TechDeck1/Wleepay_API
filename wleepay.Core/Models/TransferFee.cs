using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class TransferFee
    {
        public TransferFee() {
            TransferTypes = new Collection<TransferType>();
        }
        public int Id { get; set; }
        public double TaxAmount { get; set; }
        public double OrganizationFee { get; set; }
        public double AgentCommission { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<TransferType> TransferTypes { get; set; }

    }
}
