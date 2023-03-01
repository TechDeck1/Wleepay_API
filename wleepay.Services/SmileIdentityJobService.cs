using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;

namespace wleepay.Services
{
    public class SmileIdentityJobService : ISmileIdentityJobService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SmileIdentityJobService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SmileIdentityJob> AddJob(SmileIdentityJob job)
        {
            await _unitOfWork.SmileIdentityJob.AddAsync(job);
            return job;
        }

        public async Task<SmileIdentityJob> GetUserByJobId(string jobId)
        {
            return await _unitOfWork.SmileIdentityJob.GetByIdAsync(jobId);
        }

        public async Task<SmileIdentityJob> GetUserJob(string applicationUserId)
        {
            return await _unitOfWork.SmileIdentityJob.GetJobByUserId(applicationUserId);
        }

        public async Task<SmileIdentityJob> UpdateJobStatus(string userId, string status)
        {
            var jobToUpdate = await _unitOfWork.SmileIdentityJob.GetJobByUserId(userId);
            if (jobToUpdate != null)
            {
                jobToUpdate.LastStatus = status;
            }
            await _unitOfWork.CommitAsync();
            return jobToUpdate;
        }
    }
}
