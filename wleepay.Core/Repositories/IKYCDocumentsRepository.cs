using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IKYCDocumentsRepository:IRepository<KYCdocuments>
    {
        Task<KYCdocuments> GetKYCDocumentByUserId(string userId);
        Task<KYCdocuments> GetKYCDocumentById(int id);
    }
}
