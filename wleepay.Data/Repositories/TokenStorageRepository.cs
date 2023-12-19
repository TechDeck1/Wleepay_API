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
    public class TokenStorageRepository : Repository<TokenStorage>, ITokenStorageRepository
    {
        public TokenStorageRepository(WPContext context)
           : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<TokenStorage> GetTokenStorageById(string userId)
        {
            var tokenStorage = await WPContext.TokenStorage.Where(user => user.UserId == userId).FirstOrDefaultAsync();
            return tokenStorage;
        }
    }
}
