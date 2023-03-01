using wleepay.Core.Models;

namespace wleepay.Data.Interface
{
    public interface IUserRegistration
    {
        UserDetail AddUserDetails(UserDetail user);
        bool GetUserByPhone(string phone);
        UserDetail GetUserDetailsByPhone(string phone);
        UserDetail GetUserByID(string userId);
        UserDetail UpdateBasedOnUserID(UserDetail userDetail);
    }
}
