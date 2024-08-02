using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wleepay.Core.Enums
{
     public enum ActionsForSystemLogs
     {
        [Description("Login")] Login,
        [Description("ResetPassword")] ResetPassword,
        [Description("ForgotPassword")] ForgotPassword,
        [Description("Deactivate")] Deactivate,
        [Description("Activate")] Activate,
        [Description("CompleteLogin")] CompleteLoginProcess,
        [Description("CompleteSignUp")] CompleteSignUpProcess,
        [Description("Suspend")] Suspend,
        [Description("Unsuspend")] UnSuspend,
        [Description("BillPayment")] BillPayment,
        [Description("MobileRecharge")] MobileRecharge,


    }

}
