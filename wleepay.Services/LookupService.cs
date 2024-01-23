using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LookupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<County>> GetAllCounty()
        {
            return await _unitOfWork.County.GetAllAsync();
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentType()
        {
            return await _unitOfWork.DocumentType.GetAllAsync();
        }

        public async Task<IEnumerable<IssueType>> GetAllIssueTypes()
        {
            return await _unitOfWork.IssueType.GetAllAsync();
        }

        public async Task<IEnumerable<KYCStatus>> GetAllKYCStatus()
        {
            return await _unitOfWork.KYCStatus.GetAllAsync();
        }

        public async Task<IEnumerable<SecurityQuestions>> GetAllSecurityQuestions()
        {
            return await _unitOfWork.SecurityQuestions.GetAllAsync();
        }
       

        public async Task<IEnumerable<StaffRoles>> GetAllStaffRoles()
        {
            return await _unitOfWork.StaffRoles.GetAllAsync();
        }
    }
}
