using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wleepay.Core.Enums
{
    public enum EmailSubjectEnums
    {
        [Description("Reset password token")] ResetPassword,
        [Description("Alert: WleeSend portal password change notice (Do not reply)\"")] PasswordChanged,
        [Description("WleeSend portal account setup/activation confirmation notice (Do not reply)")] AccountSetupConfirmation,
        [Description("Failed Login attempt")] LoginFailed,
        [Description("Alert: WleeSend portal account DeActivation notice (Do not reply)")] AccountDeactivate,
        [Description("Alert: WleeSend portal account suspend/unsuspend notice (Do not reply)")] AccountSuspensionConfirmation,

    }
}
