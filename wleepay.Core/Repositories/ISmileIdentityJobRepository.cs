using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface ISmileIdentityJobRepository : IRepository<SmileIdentityJob>
    {
        Task<SmileIdentityJob> GetJobByUserId(string applicationUserId);
    }
}
