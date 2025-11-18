using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using wleepay.Core.DTO.Request;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class ValidationService : IValidationService
    {
        public string ValidateUserInput(RegisterUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.EmailId) ||
                string.IsNullOrWhiteSpace(request.PhoneNumber) ||
                string.IsNullOrWhiteSpace(request.Gender) ||
                request.CountyId == 0 ||
                string.IsNullOrWhiteSpace(request.Pin))
            {
                return "All fields are required.";
            }

            if (!Regex.IsMatch(request.EmailId, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Invalid email format.";

            return ValidatePin(request.Pin);
        }

        public string ValidatePin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
                return "PIN is required.";

            if (pin.Length < 8)
                return "PIN must be at least 8 characters.";

            if (!Regex.IsMatch(pin, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*]).{8,}$"))
                return "PIN must contain uppercase, lowercase, number, and special character.";

            return null;
        }
    }

}
