using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class CustomerTickets
    {
        public CustomerTickets() 
        {
            Notes = new Collection<Notes>();
        }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int IssueTypeId { get; set; }
        public int CustomerContact { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<Notes> Notes { get; set; }
        public bool IsSeen { get; set; }

        public virtual IssueType IssueType { get; set; }


    }
}
