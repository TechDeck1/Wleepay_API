using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface ILookupService
    {
        Task<IEnumerable<County>> GetAllCounty();
        Task<IEnumerable<DocumentType>> GetAllDocumentType();
        Task<IEnumerable<KYCStatus>> GetAllKYCStatus();
    }
}
