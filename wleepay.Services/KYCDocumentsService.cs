using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class KYCDocumentsService : IKYCDocumentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public KYCDocumentsService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<KYCdocuments> AddOrUpdateKYCDocument(KYCdocuments document)
        {
            var kycDocument = await _unitOfWork.KYCDocuments.GetKYCDocumentById(document.Id);
            
            if(kycDocument == null)
                kycDocument = document;

            if (kycDocument.Id <= 0)
                await _unitOfWork.KYCDocuments.AddAsync(kycDocument);

            await _unitOfWork.CommitAsync();
            return kycDocument;
        }

        public async Task<KYCdocuments> GetKYCDocumentById(int id)
        {
            return await _unitOfWork.KYCDocuments.GetKYCDocumentById(id);
        }

        public async Task<KYCdocuments> GetKYCDocumentByUserId(string userId)
        {
            return await _unitOfWork.KYCDocuments.GetKYCDocumentByUserId(userId);
        }
    }
}
