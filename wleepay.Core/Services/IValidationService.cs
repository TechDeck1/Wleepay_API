using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.DTO.Request;

namespace wleepay.Core.Services
{
    public interface IValidationService
    {
        string ValidateUserInput(RegisterUserRequest request);
        string ValidatePin(string pin);
    }
}
