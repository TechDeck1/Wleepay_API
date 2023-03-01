using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ICountyRepository : IRepository<County>
    {
        Task<County> GetCountyById(int id);
    }
}
