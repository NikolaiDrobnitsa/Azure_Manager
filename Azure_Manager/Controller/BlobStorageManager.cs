using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Azure.Storage.Blobs;
using System.Windows.Forms;

namespace Azure_Manager.Controller
{
    internal class BlobStorageManager
    {
        private readonly CloudBlobClient _blobClient;

        public BlobStorageManager(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public void UploadFile(string containerName, string filePath, string fileName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            using (var fileStream = File.OpenRead(filePath))
            {
                blob.UploadFromStream(fileStream);
                
            }
        }

        public void DeleteFile(string containerName, string fileName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            blob.Delete();
        }

        public List<string> GetFileList(string containerName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            return container.ListBlobs().OfType<CloudBlockBlob>().Select(blob => blob.Name).ToList();
        }

        public string GetFileContent(string containerName, string fileName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            using (var memoryStream = new MemoryStream())
            {
                blob.DownloadToStream(memoryStream);
                var content = Encoding.UTF8.GetString(memoryStream.ToArray());
                return content;
            }
        }

        
        public async void DownloadAsync(string containerName, string fileName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            string path = @"C:\Users\Nick\source\repos\Azure_Manager\Azure_Manager\Files"+ @"\"+fileName;
            //var response = await blob.DownloadToFile(path, FileMode.Create);
            blob.DownloadToFile(path, FileMode.Create);
        }


    }
}
