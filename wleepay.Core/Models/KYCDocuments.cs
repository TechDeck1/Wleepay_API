using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class KYCdocuments
    {
        public int Id { get; set; }
        public string DocumentImage { get; set; }
        public string ExtractInfo { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual DocumentType DocumentType { get; set; }
    }
}
