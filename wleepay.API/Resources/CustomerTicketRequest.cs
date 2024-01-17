namespace wleepay.API.Resources
{
    public class CustomerTicketRequest
    {
        public string PhoneNumber { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
