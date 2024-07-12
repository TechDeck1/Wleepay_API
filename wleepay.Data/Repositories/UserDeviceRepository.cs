using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class UserDeviceRepository : Repository<UserDevices>, IUserDeviceRepository
    {
        public UserDeviceRepository(WPContext context)
            : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<UserDevices> AddUserDeviceAsync(UserDevices userDeviceRequest)
        {
            var existingDevice = await WPContext.UserDevices
           .FirstOrDefaultAsync(x => x.UserId == userDeviceRequest.UserId && x.DeviceId == userDeviceRequest.DeviceId);

            if (existingDevice != null)
            {
                existingDevice.FcmToken = userDeviceRequest.FcmToken;
                WPContext.UserDevices.Update(existingDevice);
            }
            else
            {
                await WPContext.UserDevices.AddAsync(userDeviceRequest);
            }

            await WPContext.SaveChangesAsync();

            return userDeviceRequest;
        }

        public async Task<UserDevices> GetUserDevice(string UserId, string deviceId)
        {
            var userDevices = await WPContext.UserDevices.Where(x => x.UserId == UserId && x.DeviceId == deviceId).FirstOrDefaultAsync();
            return userDevices;
        }

        public async Task<IList<UserDevices>> GetUserDeviceByUserId(string UserId)
        {
            var userDevices = await WPContext.UserDevices.Where(x => x.UserId == UserId).ToListAsync();
            return userDevices;
        }
    }
}
