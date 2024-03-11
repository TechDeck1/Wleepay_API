using System;

namespace wleepay.API.Resources
{
    public class EditUserDetailResource
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string WleePhoneNumber { get; set; }
        public int StaffRoleId { get; set; }
    }
}
