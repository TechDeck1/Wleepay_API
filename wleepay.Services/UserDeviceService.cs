using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class UserDeviceService : IUserDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserDeviceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<UserDevices> AddOrUpdateUserDevices(UserDevices userDevices)
        {
			return await _unitOfWork.UserDevice.AddUserDeviceAsync(userDevices);
        }

        public async Task<UserDevices> GetUserDevice(string userId, string deviceId)
        {
            return await _unitOfWork.UserDevice.GetUserDevice(userId, deviceId);
        }
    }
}
