using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class TransferTypeRepository : Repository<TransferType>, ITransferTypeRepository
    {
        public TransferTypeRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<TransferType> GetTransferTypeById(int id)
        {
            return await WPContext.TransferTypes.Where(t=>t.Id == id).Include(t=>t.TransferFeeDetail).FirstOrDefaultAsync();
        }

        public async Task<TransferType> GetTransferTypeForTransaction(int fromWalletTypeId, int toWalletTypeId)
        {
            return await WPContext.TransferTypes
                .Where(t => t.FromWalletType == fromWalletTypeId && t.ToWalletType == toWalletTypeId)
                .FirstOrDefaultAsync();
        }
    }
}
