using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;

namespace wleepay.Data.Repositories
{
    public class TransferFeeRepository : Repository<TransferFee>, ITransferFeeRepository
    {
        public TransferFeeRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<TransferFee> GetTransferFeeDesc()
        {
            var result = await  WPContext.TransferFees.OrderByDescending(x=>x.CreatedDate).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<TransferFee>> GetAllTransferFee()
        {
            var pageSize = 0;
            var pageIndex = 0;
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var result = await WPContext.TransferFees.OrderByDescending(x => x.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();
            return result;
        }

        public async Task<TransferFee> AddTaxRates(TransferFee transferFee)
        {
            transferFee.CreatedDate= DateTime.Now;
            transferFee.IsDeleted = false;
            var result = await WPContext.AddAsync(transferFee);
            WPContext.SaveChanges();
            return await WPContext.TransferFees.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }

        public async Task<TaxRatesResponse> GetAllTransferFeeAsync(int pageIndex, int pageSize)
        {
            int takeCount = pageSize;
            int skipCount = pageIndex * pageSize;
            var result = await WPContext.TransferFees.OrderByDescending(x => x.CreatedDate)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();
            TaxRatesResponse taxRatesResponse = new TaxRatesResponse();
            taxRatesResponse.TotalCount = await WPContext.TransferFees.CountAsync();
            taxRatesResponse.TaxRates = result;
            return taxRatesResponse;
        }
    }
}
