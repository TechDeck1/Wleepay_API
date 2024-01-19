using System.Collections.Generic;
using System;
using wleepay.Core.Models;

namespace wleepay.API.Resources
{
    public class AddCustomerTicketResponseResource
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int IssueTypeId { get; set; }
        public int CustomerContact { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsSeen { get; set; }
        public ICollection<NotesResource> Notes { get; set; }
        public IssueTypeResource IssueType { get; set; }

    }
}
