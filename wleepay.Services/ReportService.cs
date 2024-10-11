using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Services;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace wleepay.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status, int pageIndex = 0, int pageSize = 10)
        {
            var result =await _unitOfWork.UserDetail.GetReportOfUserByStatusAsync(role,status,pageIndex,pageSize);
            return result;
           
        }


    }
}
