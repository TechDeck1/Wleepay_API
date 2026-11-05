using System.IO;
using System.Threading.Tasks;

namespace wleepay.Storage.Interface
{
    public interface IStorageService
    {
        Task<string> UploadData(Stream stream, string filename, string containerName, string fileExtension, string userId);
        Task<string> UploadImageWithURL(string imageURL, string containerName, string id);
        string UploadKYCDocument(string docData, string userId);
        string UploadUserProfileImage(string imageData, string userId);
    }
}
