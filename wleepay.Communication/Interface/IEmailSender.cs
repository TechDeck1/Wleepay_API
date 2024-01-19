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
        Task<bool> EmailForAccountActivatedToSystemAdmin(string email, string name, string phone);
        Task<bool> EmailForAccountActivatedToStaff(string email, string name, string phone);
        Task<bool> EmailForAccountDeactivatedToSystemAdmin(string email, string name, string phone);
        Task<bool> EmailForAccountDeactivatedToStaff(string email, string name, string phone);
        Task<bool> EmailForAccountCreationToSystemAdmins(string systemAdminEmails, string userEmail);
        Task<bool> ReplyForCustomerTicket(string Message, string email);
        Task<bool> ReplyForCustomerTicketClosed(string Message, string email);


    }
}
