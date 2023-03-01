using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class ApplicationUserResource : UserAccountResource
    {
        public IEnumerable<KYCDocumentResource> KYCdocuments { get; set; }
    }
}
