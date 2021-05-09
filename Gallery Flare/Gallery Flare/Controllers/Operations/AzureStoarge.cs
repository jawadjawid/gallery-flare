using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Gallery_Flare.Controllers.Operations
{
    public class AzureStoarge
    {
        private CloudStorageAccount storageAccount = CloudStorageAccount.Parse("");
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;

        public AzureStoarge()
        {
            blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference("flare");
        }

        public async Task<string> UploadAsync(Stream file, string name)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference($"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}_{name}");
            await blob.UploadFromStreamAsync(file);
            return blob.Uri.AbsoluteUri;
        }
    }
}
