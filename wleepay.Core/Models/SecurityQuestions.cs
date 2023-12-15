using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace wleepay.Core.Models
{
    public class SecurityQuestions
    {

        public SecurityQuestions()
        {
            UserDetails = new Collection<UserDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Question { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}

