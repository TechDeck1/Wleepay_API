using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Response;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IReportService
    {
        Task<UserDetailsResponseResource> GetReportOfUserByStatusAsync(int role, int status, int pageIndex = 0, int pageSize = 10);
    }
}
