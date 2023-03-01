using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IKYCLevelService
    {
        Task<IEnumerable<KYCLevel>> GetAll();
        Task<KYCLevel> GetById(int id);
    }
}
