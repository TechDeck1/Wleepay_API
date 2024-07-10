using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IFirebaseNotification
    {
        string SendFirebaseMessageRequest(FirebaseMessageRequest messageRequest);

    }
}
