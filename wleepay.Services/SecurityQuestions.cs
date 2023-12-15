using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Services;
using wleepay.Core;
using System.Threading.Tasks;
using System.Collections;

namespace wleepay.Services
{
    public class SecurityQuestionsService: ISecurityQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SecurityQuestionsService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Models.SecurityQuestions> GetAllSecurityQuestions()
        {
            var result=  _unitOfWork.SecurityQuestions.GetAllSecurityQuestions();
            return result;
        }
    }
}
