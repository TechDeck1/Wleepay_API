using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core;
using wleepay.Core.Services;
using System.Security.AccessControl;

namespace wleepay.Services
{
    public class VerificationService : IVerificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public VerificationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Verification> AddVerification(Verification verification)
        {
            verification.CreatedDate = DateTime.Now;
            var verificationSource = await _unitOfWork.Verification.AddVerification(verification);
            return verificationSource;
        }

        public async Task<Verification> GetVerificationByPhone(string phoneNumber)
        {
            var verification = await _unitOfWork.Verification.GetVerificationByPhone(phoneNumber);
            return verification;
        }
    }
}
