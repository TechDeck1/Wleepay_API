using System;
using wleepay.Core.Enums;

namespace wleepay.API.Resources
{
    public class AddStaffDetailsResource
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
        public string DocumentImage { get; set; }
        public string WleePhoneNumber { get; set; }
        public int? KYCLevel { get; set; }
        public string KYCDocumentImage { get; set; }
        public int SecurityQuestionId { get; set; }
        public int StaffRoleId { get; set; }
        public string SecurityAnswerText { get; set; }
        //public bool RememberMe { get; set; } = false; // Default value is set to false
        public Role? Role { get; set; }
        public string CreatedBy { get; set; }
        public DateTime HireDate { get; set; }
    }
}
