using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace wleepay.Core.DTO.Request
{
    public class EmailLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Pin { get; set; }
    }
}
