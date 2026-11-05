using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> LoginAsync(EmailLoginRequest request);
        Task<AuthenticationResponse> RegisterAsync(RegisterUserRequest request);
        Task<GenericResponse> ResetPinAsync(ResetPinRequest request, string userId);
        Task<GenericResponse> ForgotPinAsync(ForgotPinRequest request);

        Task<GenericResponse> SendDualEmailOtpAsync(SendDualEmailOtpRequest request);
        Task<GenericResponse> VerifyDualEmailOtpAsync(VerifyDualEmailOtpRequest request);
    }

}
