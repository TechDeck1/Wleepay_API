using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }


        public AuthenticationResponse() { }

        public AuthenticationResponse(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }
    }
}
