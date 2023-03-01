using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            KYCdocuments = new Collection<KYCdocuments>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KYCdocuments> KYCdocuments { get; set; }
    }
}
