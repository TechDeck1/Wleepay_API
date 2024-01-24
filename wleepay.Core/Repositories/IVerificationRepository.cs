using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IVerificationRepository : IRepository<Verification>
    {
        Task<Verification> AddVerification(Verification verification);
        Task<Verification> GetVerificationByPhone(string phoneNumber);
    }
}
