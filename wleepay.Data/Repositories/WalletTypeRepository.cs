using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class WalletTypeRepository : Repository<WalletType>, IWalletTypeRepository
    {
        public WalletTypeRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }
    }
}
