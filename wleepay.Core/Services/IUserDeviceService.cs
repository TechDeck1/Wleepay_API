using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IUserDeviceService
    {
        Task<UserDevices> AddOrUpdateUserDevices(UserDevices userDevices);
        Task<UserDevices> GetUserDevice(string userId, string deviceId);
        Task<IList<UserDevices>> GetUserDeviceByUserId(string userId);
        Task<bool> DeleteUserDevice(string userId, string deviceId);

    }
}
