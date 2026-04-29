using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class IssueType
    {
        public IssueType()
        {
            CustomerTickets = new Collection<CustomerTickets>();
        }
      
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CustomerTickets> CustomerTickets { get; set; }

    }
}
