using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class UserNotes
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public string userId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string AddedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
