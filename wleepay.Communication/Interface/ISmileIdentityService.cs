using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wleepay.Communication.Interface
{
    public interface ISmileIdentityService
    {
        Task<bool> DeleteUser(string userId);
        Task<bool> ReEnrollUser(string userId);
    }
}
