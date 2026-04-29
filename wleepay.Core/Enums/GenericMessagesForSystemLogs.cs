using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
namespace wleepay.Core.Enums
{
    public enum GenericMessagesForSystemLogs
    {
        [Description("User is succesfully logged in.")] SuccessfullyLoggedIn,
        [Description("WleeSend portal account is locked after 3 failed login attempts.")] AccountLocked,
        [Description("WleeSend portal account is De-Activated.")] AccountDeActivated,
        [Description("WleeSend portal account is Activated.")] AccountActivated,
        [Description("WleeSend portal account is Suspended.")] AccountSuspended,
        [Description("WleeSend portal account is unsuspended.")] AccountUnsuspended,
        [Description("Login process is completed.")] CompleteLoginProcess,
        [Description("Email is not exists.")] NotExists,
        [Description("process id failed.")] Failed,
        [Description("User entered wrong otp.")] FalseLoginAttempt,
        [Description("Bill payment is successfully done")] BillPayment,
        [Description("Mobile recharge is successfully done")] MobileRecharge,
    }
}
