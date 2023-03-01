using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ITransferFeeService
    {
        Task<TransferFee> GetTransferFeeById(int id);
        Task<TransferFee> UpdateTransferFee(TransferFee fee);
    }
}
