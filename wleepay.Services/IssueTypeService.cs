using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    internal class IssueTypeService : IIssueTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IssueTypeService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<IssueType> GetAllIssueTypes()
        {
            var result = _unitOfWork.IssueType.GetAllIssueTypes();
            return result;
        }

       

    }
}
