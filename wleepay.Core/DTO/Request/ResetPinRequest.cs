using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class ResetPinRequest
    {
        [Required]
        public string OldPin { get; set; }

        [Required]
        public string NewPin { get; set; }

        public string ConfirmPin { get; set; }
    }
}
