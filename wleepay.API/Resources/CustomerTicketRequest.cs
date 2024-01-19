namespace wleepay.API.Resources
{
    public class CustomerTicketRequest
    {
        public string PhoneNumber { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class CustomerTicketSeenStatusRequest
    {
        public int CustomerTicketId { get; set; }
        public bool IsSeen { get; set; }
    }
}
