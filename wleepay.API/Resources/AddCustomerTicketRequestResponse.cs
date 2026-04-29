using System.Collections.Generic;
using System;

namespace wleepay.API.Resources
{
    public class AddCustomerTicketRequestResponse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int IssueTypeId { get; set; }
        public int CustomerContact { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int NotificationStatus { get; set; }
        public ICollection<NotesResource> Notes { get; set; }
    }
}
