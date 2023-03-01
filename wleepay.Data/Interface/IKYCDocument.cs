using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Interface
{
    public interface IKYCDocument
    {
        IEnumerable<DocumentType> GetAllKycDocument();
        KYCdocuments AddKYCDoc(KYCdocuments kycdocuments);
        KYCdocuments GetKYCDocByID(string UserID);
        string GetKYCDocNameByDocID(int docID);
        KYCdocuments UpdateKYCDocument(KYCdocuments kycdocument);
    }
}
