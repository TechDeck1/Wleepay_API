using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;
using wleepay.Core.Enums;

namespace wleepay.Services
{
    public class TransferTypeService : ITransferTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferTypeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<County>> GetAll()
        {
            return await _unitOfWork.County.GetAllAsync();
        }

        public async Task<TransferType> GetTransferTypeById(int id)
        {
            return await _unitOfWork.TransferType.GetTransferTypeById(id);
        }

        public async Task<TransferType> GetTransferTypeForTransaction(int fromWalletTypeId, int toWalletTypeId)
        {
            return await _unitOfWork.TransferType.GetTransferTypeForTransaction(fromWalletTypeId, toWalletTypeId);
        }    
        
        public async Task<IList<TransferType>> GetTransferTypeList(int fromWalletTypeId, int toWalletTypeId)
        {
            return await _unitOfWork.TransferType.GetTransferTypeList(fromWalletTypeId, toWalletTypeId);
        }

        public async  Task<TransferType> UpdateTransferTypeById(TransferType request)
        {

            return await _unitOfWork.TransferType.UpdateTransferTypeById(request);
        }
    }
}
