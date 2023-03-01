using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ISmileIdentityJobService
    {
        Task<SmileIdentityJob> GetUserJob(string applicationUserId);
        Task<SmileIdentityJob> AddJob(SmileIdentityJob job);
        Task<SmileIdentityJob> UpdateJobStatus(string userId, string status);
        Task<SmileIdentityJob> GetUserByJobId(string jobId);
    }
}
