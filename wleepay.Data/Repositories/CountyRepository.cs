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
    public class CountyRepository : Repository<County>, ICountyRepository
    {
        public CountyRepository(DbContext context):base(context)
        {

        }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<County> GetCountyById(int id)
        {
            return await WPContext.County.Where(county => county.Id == id).FirstOrDefaultAsync();
        }
    }
}
