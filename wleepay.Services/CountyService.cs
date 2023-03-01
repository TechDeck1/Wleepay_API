using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;

namespace wleepay.Services
{
    public class CountyService : ICountyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountyService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<County>> GetAll()
        {
            return await _unitOfWork.County.GetAllAsync();
        }
    }
}
