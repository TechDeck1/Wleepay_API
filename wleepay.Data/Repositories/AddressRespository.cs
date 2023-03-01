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
        public class AddressRespository : Repository<Address>, IAddressRepository
        {
            public AddressRespository(DbContext context) : base(context)
            {

            }

            private WPContext WPContext
            {
                get { return Context as WPContext; }
            }

            public async Task<Address> GetAddressByUserId(string userId)
            {
                return await WPContext.Addresses.Where(a => a.UserId == userId).FirstOrDefaultAsync();
            }
        }
    }
