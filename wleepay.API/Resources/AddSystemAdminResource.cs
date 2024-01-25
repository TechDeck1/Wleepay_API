using System;
using wleepay.Core.Enums;

namespace wleepay.API.Resources
{
    public class AddSystemAdminResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public DateTime? DOB { get; set; }
        public int KYCStatusId { get; set; }
        public string Image { get; set; }
        public int CountyId { get; set; }
        public string DocumentNumber { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? KYCLevel { get; set; }
        public string KYCDocumentImage { get; set; }
        public Role? Role { get; set; }
        public string CreatedBy { get; set; }

    }
}
