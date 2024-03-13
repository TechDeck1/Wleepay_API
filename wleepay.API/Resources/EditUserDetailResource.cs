using System;
using wleepay.Core.Enums;

namespace wleepay.API.Resources
{
    public class EditUserDetailResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string WleePhoneNumber { get; set; }
        public int StaffRoleId { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Image { get; set; }
        public int CountyId { get; set; }
        public string DocumentNumber { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentImage { get; set; }
        public string KYCDocumentImage { get; set; }
        public int SecurityQuestionId { get; set; }
        public DateTime HireDate { get; set; }
    }
}
