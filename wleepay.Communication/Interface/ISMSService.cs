using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Communication.Interface
{
    public interface ISMSService
    {
        string sendMessage(string message, string phoneNumber);
        string sendSmsToCustomerForTicketClose(string message, string phoneNumber);
        string replyCustomerForCustomerTicket(string message, string phoneNumber);

    }
}
