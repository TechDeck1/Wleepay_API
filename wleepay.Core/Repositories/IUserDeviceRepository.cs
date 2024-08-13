using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IUserDeviceRepository : IRepository<UserDevices>
    {
        Task<UserDevices> AddUserDeviceAsync(UserDevices usersStaffRole);
        Task<UserDevices> GetUserDevice(string UserId, string deviceId);
        Task<bool> DeleteUserDevice(string UserId, string deviceId);
        Task<IList<UserDevices>> GetUserDeviceByUserId(string UserId);
    }
}
