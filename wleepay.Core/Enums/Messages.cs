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
        [Description("Invalid request")] NotFound,
        [Description("Invalid combination of email and password")] EmailNotFound,
        [Description("Invalid login request.")] BadRequest,
        [Description("Your account is unlocked by system administrator.")] UnlockAccount,
        [Description("Verification is successfull")] VerificationSuccessfull,
        [Description("You entered wrong OTP! please try again.")] WrongOtp,
        [Description("Your OTP is expired! please try again.")] OtpExpired,
        [Description("You can't create Super Admin.")] IsSuperAdmin,
        [Description("Email is already in use.")] EmailAlreadyUsed,
        [Description("Phone number is already in use.")] PhoneAlreadyUsed,
        [Description("Phone number and email is already in use.")] PhoneAndEmailAlreadyUsed,
        [Description("Can't create user.")] UserNotCreated,
        [Description("Link has been sent to your email to reset your password.")] LinkSentForResetPassword,
        [Description("Password is successfully updated.")] PasswordUpdated,
        [Description("Password is successfully genrated.")] PasswordGenerated,
        [Description("Email is not valid")] EmailNotValid,
        [Description("Please correct the entered details and try again.")] DetailsNotCorrect,
        [Description("Your account is not setup yet. Please setup your account first")] AccountNotSetup,
        [Description("Invalid Request")] InvalidRequest,
        [Description("Account is already setup")] AccountAlreadySetup,
        [Description("Account is not setup yet. please setup your account")] AccountNotAlreadySetup,
        [Description("Link is expired. Please generate new link.")] LinkExpired,
        [Description("please reset your password")] ResetPassword,
        [Description("The above Wleepay staff’s portal account is now DeActivated.")] AccountDeactivated,
        [Description("Phone number does not exist. please enter registered number.")] PhoneNumberNotExisted,
        [Description("Support ticket has been created.")] CustomerTicketCreated,
        [Description("No Ticket found.")] TicketNotFound,
        [Description("Message is sent!")] MessageSent,
        [Description("Ticket has been closed.")] TicketClosed,
        [Description("Ticket Created.")] TicketCreated,
        [Description("Ticket has been updated.")] TicketUpdated,
        [Description("OTP is sent successfully")] OTPSent,
        [Description("Ticket Closed.")] SubjectForTicketClosed,
        [Description("Reply for Ticket.")] SubjectForTicketUpdated,
        [Description("You cannot login from this screen.")] WrongLoginPage,
        [Description("Information is succesfully updated.")] InfoUpdated,
        [Description("Invalid access token or refresh token.")] InvalidAccessToken,
        [Description("Phone is not verified please verify your phone number.")] PhoneNumberNotVerified,
        [Description("Cannont update status id is null.")] NotUpdated,
        [Description("Your account is logged in on other device.")] UserLoggedInOtherDevice,
        [Description("Meter number is incorrect.")] MeterNumberIncorrect,
        [Description("Payment is failed.")] PaymentFailed,
        [Description("Message is sent to all the agents.")] smsSentToAgents,
        [Description("Message is sent to all the members.")] smsSentToSubscribers,

    }
}
