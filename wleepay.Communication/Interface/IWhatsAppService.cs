using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Communication.Interface
{
    public interface IWhatsAppService
    {
        string sendMessageForCustomerTicket(string message, string phoneNumber);
    }
}
