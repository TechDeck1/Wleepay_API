using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int CustomerTicketId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } 
        public string AddedBy { get; set; }
        public bool IsDeleted { get; set; }
        
        public virtual CustomerTickets CustomerTickets { get; set; }

    }
}
