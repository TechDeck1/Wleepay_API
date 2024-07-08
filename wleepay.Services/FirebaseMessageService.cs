using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.DTO.Request;
using wleepay.Core.Services;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class FirebaseMessageService : IFirebaseNotification
    {
        public FirebaseMessageService() { }

        public async Task<string> SendFirebaseMessageRequest(FirebaseMessageRequest firebaseMessageRequest)
        {
            try
            {
                var message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = firebaseMessageRequest.Title,
                        Body = firebaseMessageRequest.Body,
                    },
                    Data = new Dictionary<string, string>()
                    {
                        ["FirstName"] = firebaseMessageRequest.FirstName,
                        ["LastName"] = firebaseMessageRequest.LastName
                    },
                    Token = firebaseMessageRequest.DeviceToken
                };

                var messaging = FirebaseMessaging.DefaultInstance;
                var result = await messaging.SendAsync(message);

                if (!string.IsNullOrEmpty(result))
                {
                    // Message was sent successfully
                    return "Message sent successfully!";
                }
                else
                {
                    // There was an error sending the message
                    throw new Exception("Error sending the message.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
