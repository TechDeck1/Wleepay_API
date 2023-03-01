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
    public class TransferFeeRepository : Repository<TransferFee>, ITransferFeeRepository
    {
        public TransferFeeRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }
    }
}
