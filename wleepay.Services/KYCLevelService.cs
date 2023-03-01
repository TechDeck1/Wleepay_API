using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class KYCLevelService : IKYCLevelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KYCLevelService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KYCLevel>> GetAll()
        {
            return await _unitOfWork.KYCLevel.GetAllAsync();
        }

        public async Task<KYCLevel> GetById(int id)
        {
            return await _unitOfWork.KYCLevel.GetByIdAsync(id);
        }
    }
}
