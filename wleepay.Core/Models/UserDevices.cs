using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace wleepay.Core.Models
{
    public class UserDevices
    {
        public int Id { get; set; }
        public string UserId { get; set; }  
        public string DeviceId { get; set; }
        public string FcmToken { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
