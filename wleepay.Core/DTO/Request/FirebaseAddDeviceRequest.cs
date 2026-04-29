using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class FirebaseAddDeviceRequest
    {
        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }
        public string UserId { get; set; }
    }

    public class FirebaseDeleteDeviceRequest
    {
        public string DeviceId { get; set; }
        public string UserId { get; set; }
    }
}
