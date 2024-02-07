using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;
using wleepay.Core.DTO.Response;

namespace wleepay.Services
{
    public class TransferFeeService : ITransferFeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferFeeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TransferFee> AddTransferFee(TransferFee transferFee)
        {
            return await _unitOfWork.TransferFee.AddTaxRates(transferFee);
        }

        public async Task<TaxRatesResponse> GetAllTransferFees(int index, int pageSize)
        {
            return await _unitOfWork.TransferFee.GetAllTransferFeeAsync(index, pageSize);
        }

        public async Task<TransferFee> GetTransferFeeByDesc()
        {
            return await _unitOfWork.TransferFee.GetTransferFeeDesc();
        }

        public async Task<TransferFee> GetTransferFeeById(int id)
        {
            return await _unitOfWork.TransferFee.GetByIdAsync(id);
        }

        public async Task<TransferFee> UpdateTransferFee(TransferFee fee)
        {
            var transferFee = await _unitOfWork.TransferFee.GetByIdAsync(fee.Id);
            if(transferFee != null)
            {
                transferFee = fee;
            }
            await _unitOfWork.CommitAsync();
            return transferFee;
        }
    }
}
