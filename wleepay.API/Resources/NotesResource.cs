using System;

namespace wleepay.API.Resources
{
    public class NotesResource
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int CustomerTicketId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
