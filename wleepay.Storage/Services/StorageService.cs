using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using wleepay.Storage.Interface;

namespace wleepay.Storage.Services
{
     public class StorageService : IUploadDocument
    {
        public readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger <StorageService> _logger;
        public StorageService(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<StorageService> logger)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }
        public async Task<string> UploadData(Stream stream, string filename, string containerName, string fileExtension, string userId)
        {
            try
            {
                if (fileExtension.Equals(".pdf") || fileExtension.Equals(".txt") || fileExtension.Equals(".png") || fileExtension.Equals(".jpg") || fileExtension.Equals(".jpeg") || fileExtension.Equals(".doc") || fileExtension.Equals(".docx"))
                {
                    var header = new BlobHttpHeaders();

                    if (fileExtension == ".pdf")
                        header.ContentType = "application/pdf";
                    else if (fileExtension == ".txt")
                        header.ContentType = "text/plain";
                    else if (fileExtension == ".png")
                        header.ContentType = "image/png";
                    else if (fileExtension == ".jpg")
                        header.ContentType = "image/jpg";
                    else if (fileExtension == ".jpeg")
                        header.ContentType = "image/jpeg";
                    else if (fileExtension == ".doc")
                        header.ContentType = "application/msword";
                    else if (fileExtension == ".docx")
                        header.ContentType = "application/msword";

                    //var connectionString = Configuration.GetSection("AzureStorage").GetSection("AzureConnectionString").Value;
                    //CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                    //CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                    //CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
                    //CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);
                    //cloudBlockBlob.Properties.ContentType = header.ContentType;
                    //await cloudBlockBlob.UploadFromStreamAsync(stream);

                    //stream.Dispose();
                    //return ($"{containerName}/{cloudBlockBlob.Name}"); 


                    //string contentRootPath = _hostingEnvironment.ContentRootPath;
                    string contentRootPath = _hostingEnvironment.WebRootPath;
                    _logger.LogInformation($"WebRootPath: {contentRootPath}");
                    string path = "";
                    path = Path.Combine(contentRootPath, "Images");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //or path = Path.Combine(contentRootPath , "wwwroot" ,"CSS" );
                    var folderPath = path;
                    //@$"D:\new wleepay\wleepay\wleepay.API\Images";
                    //if (!Directory.Exists(folderPath))
                    //{
                    //    Directory.Delete(folderPath);
                    //}

                    using (System.IO.FileStream file = new FileStream(Path.Combine(folderPath, userId+".jpg"), FileMode.Create, FileAccess.ReadWrite))
                    {
                        stream.CopyTo(file);
                    }
                    _logger.LogInformation($"Path: {Path.Combine(folderPath, userId)} Exists: {File.Exists(Path.Combine(folderPath, userId))}");
                    //return System.Convert.ToBase64String((System.IO.File.ReadAllBytes(Path.Combine(folderPath, userId))));
                    return Path.Combine(folderPath, userId+ ".jpg");
                }


                else
                {
                    return "File Format Not Supported.";
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<string> UploadImageWithURL(string imageURL, string containerName, string id)
        {            try            {
                //var header = new BlobHttpHeaders();
                var fileExactPath = imageURL.Split("?")?[0];
                var fileExtension = Path.GetExtension(fileExactPath);
                string fileName = $"{Guid.NewGuid().ToString()}_{Path.GetFileName(fileExactPath)}";

                //if (fileExtension == ".png")
                //    header.ContentType = "image/png";
                //else if (fileExtension == ".jpg")
                //    header.ContentType = "image/jpg";
                //else if (fileExtension == ".jpeg")
                //    header.ContentType = "image/jpeg";
                //var connectionString = Configuration.GetSection("AzureStorage").GetSection("AzureConnectionString").Value;
                //CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                //CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                //CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

                //CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                //cloudBlockBlob.Properties.ContentType = header.ContentType;

                //string contentRootPath = _hostingEnvironment.ContentRootPath;

                //string path = "";
                //path = Path.Combine(contentRootPath, "Images");
                ////or path = Path.Combine(contentRootPath , "wwwroot" ,"CSS" );
                //var folderPath = path;
                //@$"D:\new wleepay\wleepay\wleepay.API\Images";

                //using (var client = new HttpClient())
                //{
                //    //get the url 
                //    var request = new HttpRequestMessage(HttpMethod.Get, imageURL);
                //    var sendTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                //    var response = sendTask.Result;

                //    HttpStatusCode status = response.StatusCode;
                //    if (status == HttpStatusCode.OK)
                //    {
                //        var httpStream = response.Content.ReadAsStreamAsync().Result;
                //        MemoryStream ms = new MemoryStream();
                //        httpStream.CopyTo(ms);
                //        ms.Position = 0;

                //        using (System.IO.FileStream file = new FileStream(Path.Combine(folderPath, id+ fileExtension), FileMode.Create, FileAccess.ReadWrite))
                //        {
                //            ms.CopyTo(file);
                //        }


                //        //await cloudBlockBlob.UploadFromStreamAsync(ms);
                //    }
                //}

                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                string contentRootPath = _hostingEnvironment.WebRootPath;
                var folderPath = Path.Combine(contentRootPath, "Images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                
                using (var client = new HttpClient())
                {
                    //get the url 
                    var request = new HttpRequestMessage(HttpMethod.Get, imageURL);
                    var sendTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    var response = sendTask.Result;

                    HttpStatusCode status = response.StatusCode;
                    if (status == HttpStatusCode.OK)
                    {
                        var httpStream = response.Content.ReadAsStreamAsync().Result;
                        MemoryStream ms = new MemoryStream();
                        httpStream.CopyTo(ms);
                        ms.Position = 0;

                        using (System.IO.FileStream file = new FileStream(Path.Combine(folderPath, id + fileExtension), FileMode.Create, FileAccess.ReadWrite))
                        {
                            ms.CopyTo(file);
                        }


                        //await cloudBlockBlob.UploadFromStreamAsync(ms);
                    }
                }
                return (Path.Combine(folderPath, id + fileExtension));                //return ($"{containerName}/{cloudBlockBlob.Name}");            }            catch (Exception ex)            {                throw ex.InnerException;            }        }

        public string UploadKYCDocument(string docData, string userId)
        {
            try
            {
                var docFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "KYCDocument");
                byte[] image = Convert.FromBase64String(docData);
                File.WriteAllBytes(Path.Combine(docFolderPath,$"{userId}.jpg"), image);
                return Path.Combine(docFolderPath, $"{userId}.jpg");
            }
            catch(Exception ex)
            {
                return "";
            }
            
        }


    }
}
