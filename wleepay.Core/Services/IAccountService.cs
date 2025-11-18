using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace wleepay.Core.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> LoginAsync(EmailLoginRequest request);
        Task<AuthenticationResponse> RegisterAsync(RegisterUserRequest request);
        Task<GenericResponse> ResetPinAsync(ResetPinRequest request, string userId);
        Task<GenericResponse> ForgotPinAsync(ForgotPinRequest request);
    }

}
