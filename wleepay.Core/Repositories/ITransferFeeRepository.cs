using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ITransferFeeRepository : IRepository<TransferFee>
    {
        Task<TransferFee> GetTransferFeeDesc();
        Task<TaxRatesResponse> GetAllTransferFeeAsync(int index, int pageSize);
        Task<IEnumerable<TransferFee>> GetAllTransferFee();
        Task<TransferFee> AddTaxRates(TransferFee transferFee);

    }
}
