using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class KYCDocumentsRepository: Repository<KYCdocuments>, IKYCDocumentsRepository
    {
        public KYCDocumentsRepository(DbContext context):base(context)
        {

        }
        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<KYCdocuments> GetKYCDocumentById(int id)
        {
            return await WPContext.KYCDocuments.Where(document => document.Id == id).FirstOrDefaultAsync();
        }

        public async Task<KYCdocuments> GetKYCDocumentByUserId(string userId)
        {
            return await WPContext.KYCDocuments.Where(document => document.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
