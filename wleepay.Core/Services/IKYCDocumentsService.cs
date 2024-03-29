using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IKYCDocumentsService
    {
        Task<KYCdocuments> AddOrUpdateKYCDocument(KYCdocuments document);
        Task<KYCdocuments> UpdateKYCDocument(KYCdocuments document);
        Task<KYCdocuments> GetKYCDocumentByUserId(string userId);
        Task<KYCdocuments> GetKYCDocumentById(int id);
    }
}
