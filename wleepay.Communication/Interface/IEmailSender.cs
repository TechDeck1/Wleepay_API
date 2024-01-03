using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Messaging;

namespace wleepay.Communication.Interface
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsyncForLockAccount(string email);
        Task<bool> SendForgotPasswordEmailLink(string email, string uri, string username);
        Task<bool> SendEmailToSystemAdminstrator(string email);
        Task<bool> SendEmailToStaffToCompleteLoginProcess(string email, string uri, string username);
        Task<bool> EmailAccountSuccessfullySetup(string email, string password);
        Task<bool> EmailForAccountActivatedToSystemAdmin(string email, string name, string phone);
        Task<bool> EmailForAccountDeactivatedToSystemAdmin(string email, string name, string phone);
        Task<bool> EmailForAccountCreationToSystemAdmins(string systemAdminEmails, string userEmail);

    }
}
