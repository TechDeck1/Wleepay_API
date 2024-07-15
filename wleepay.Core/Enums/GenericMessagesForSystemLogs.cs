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
        [Description("Wleepay portal account is locked after 3 failed login attempts.")] AccountLocked,
        [Description("Wleepay portal account is De-Activated.")] AccountDeActivated,
        [Description("Wleepay portal account is Activated.")] AccountActivated,
        [Description("Wleepay portal account is Suspended.")] AccountSuspended,
        [Description("Wleepay portal account is unsuspended.")] AccountUnsuspended,
        [Description("Login process is completed.")] CompleteLoginProcess,
        [Description("Email is not exists.")] NotExists,
        [Description("process id failed.")] Failed,
        [Description("User entered wrong otp.")] FalseLoginAttempt
    }
}
