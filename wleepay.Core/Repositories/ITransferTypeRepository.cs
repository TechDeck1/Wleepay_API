using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ITransferTypeRepository : IRepository<TransferType>
    {
        Task<TransferType> GetTransferTypeById(int id);
        Task<TransferType> GetTransferTypeForTransaction(int fromWalletTypeId, int toWalletTypeId);
    }
}
