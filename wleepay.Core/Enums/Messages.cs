using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace wleepay.Core.Enums
{
    public enum Messages
    {
        [Description("User is succesfully logged in")] SuccessfullyLoggedIn,
        [Description("You have 3 failed login attemps. Your Wleepay portal account is locked. You must contact system administrator to unlock your account")] AccountLocked,
        [Description("Your Account is de-activated, please Contact to your system adminintrator.")] AccountDeActivated,
        [Description("No user found.")] NotFound,
        [Description("Invalid login request.")] BadRequest,
        [Description("Your account is unlocked by system administrator.")] UnlockAccount,
        [Description("Verification is successfull")] VerificationSuccessfull,
        [Description("You entered wrong OTP! please try again.")] WrongOtp,
        [Description("Your OTP is expired! please try again.")] OtpExpired,
        [Description("You can't create Super Admin.")] IsSuperAdmin,
        [Description("Email is already in use.")] EmailAlreadyUsed,
        [Description("Phone number is already in use.")] PhoneAlreadyUsed,
        [Description("Can't create user.")] UserNotCreated,
        [Description("Link has been sent to your email to reset your password.")] LinkSentForResetPassword,
        [Description("Password is successfully updated.")] PasswordUpdated,
        [Description("Password is successfully genrated.")] PasswordGenerated,
        [Description("Email is not valid")] EmailNotValid,
        [Description("Please correct the entered details and try again.")] DetailsNotCorrect,
        [Description("Invalid Request")] InvalidRequest,

    }
}
