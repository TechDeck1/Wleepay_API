using System.Collections.Generic;
using System;

namespace wleepay.API.Resources
{
    public class CustomerTicketReplyResource
    {
        public int CustomerTicketId { get; set; }
        public string UserId { get; set; }
        public ICollection<NotesResource> Notes { get; set; }
    }
}
