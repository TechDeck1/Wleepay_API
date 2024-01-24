using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IVerificationService
    {
        Task<Verification> AddVerification(Verification verification);
        Task<Verification> GetVerificationByPhone(string phoneNumber);
    }
}
