using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public MessageType MessageType { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }


    public class UserNotesReponse
    {
        public int TotalCount { get; set; }
        public List<UserNotes> UserNotes { get; set; }
    }

    public enum MessageType
    {
        Note = 1,
        SMS = 2
    }


}
