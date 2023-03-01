using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using wleepay.Storage.Services;

namespace wleepay.Storage.Interface
{
    public interface IUploadDocument
    {
        Task<string> UploadData(Stream stream, string filename, string containerName, string fileExtension, string userId);
        Task<string> UploadImageWithURL(string imageURL, string containerName, string id);
        string UploadKYCDocument(string docData, string userId);
    }
}
