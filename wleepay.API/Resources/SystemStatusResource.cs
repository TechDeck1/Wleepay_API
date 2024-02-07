using System;

namespace wleepay.API.Resources
{
    public class SystemStatusResource
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
