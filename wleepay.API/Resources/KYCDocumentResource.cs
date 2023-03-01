using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class KYCDocumentResource
    {
        public int Id { get; set; }
        public string DocumentImage { get; set; }
        public string ExtractInfo { get; set; }
        public string UserId { get; set; }
        public int Type { get; set; }
        public string DocumentNumber { get; set; }
        public KYCLevel KYCLevelDetail { get; set; }
        public virtual DocumentTypeResource DocumentType { get; set; }
    }
}
