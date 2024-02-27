using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace wleepay.Communication.Interface
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsyncForLockAccountToSystemAdmin(string email, string name , string phone);
        Task<bool> SendEmailAsyncForLockAccountToStaff(string email, string name, string phone);
        Task<bool> SendForgotPasswordEmailLink(string email, string uri, string username);
        Task<bool> SendPasswordChangeEmailToSystemAdminstrator(string email, string name, string phone);
        Task<bool> SendPasswordChangeEmailToStaff(string email, string name, string phone);
        Task<bool> SendEmailToStaffToCompleteLoginProcess(string email, string uri, string username);
        Task<bool> EmailForAccountActivatedToSystemAdmin(string email, string name, string phone, string permissions);
        Task<bool> EmailForAccountActivatedToStaff(string email, string name, string phone);
        Task<bool> EmailForAccountDeactivatedToSystemAdmin(string email, string name, string phone, string permissions);
        Task<bool> EmailForAccountDeactivatedToStaff(string email, string name, string phone);
        Task<bool> EmailForAccountCreationToSystemAdmins(string systemAdminEmails, string userEmail);
        Task<bool> ReplyForCustomerTicket(string Message, string email, string subject);
        Task<bool> SendEmailToSystemAdminToCompleteSignUp(string email, string uri, string username);
        Task<bool> EmailForAccountCreationToSystemAdmin(string email, string name, string phone);
        Task<string> EmailForPaymentRecieved(string messageText, string email);
        Task<string> SendEmailAsync(string email,string template, string username, string mailSubject);


    }
}
