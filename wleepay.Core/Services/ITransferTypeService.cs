using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ITransferTypeService
    {
        Task<TransferType> GetTransferTypeById(int id);
        Task<TransferType> UpdateTransferTypeById(TransferType id);
        Task<TransferType> GetTransferTypeForTransaction(int fromWalletTypeId, int toWalletTypeId);
    }
}
