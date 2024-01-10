using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Communication.Interface
{
    public interface ISMSService
    {
        string sendMessage(string message, string phoneNumber);
        string sendSmsForAccountDeactivation (string message, string phoneNumber);
    }
}
