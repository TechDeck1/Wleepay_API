using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class SystemActivityLog
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Action { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
