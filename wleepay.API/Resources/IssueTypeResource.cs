using System;

namespace wleepay.API.Resources
{
    public class IssueTypeResource
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
