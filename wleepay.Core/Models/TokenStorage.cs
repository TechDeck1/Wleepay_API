using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class TokenStorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
        
    }
}
