using System.Collections.Generic;

namespace wleepay.API.Resources
{
    public class CustomerTicketCloseResource
    {
        public int CustomerTicketId { get; set; }
        public string UserId { get; set; }
        public ICollection<NotesResource> Notes { get; set; }
    }
}
