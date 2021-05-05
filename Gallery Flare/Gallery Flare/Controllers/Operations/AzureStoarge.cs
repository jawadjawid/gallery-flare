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
        private CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=galleryflare;AccountKey=qEzmObr5GKhv8XTXXl0PEOAQ3J/hVZM+tbpllTbRv1uHK76OCeeD0AufUsyoguYeaidqTWKEkl2nbB1LMK6wLw==;EndpointSuffix=core.windows.net");
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;

        public AzureStoarge()
        {
            blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference("flare");
        }

        public async Task<string> UploadAsync(Stream file, string name)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference($"{name}_{DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
            await blob.UploadFromStreamAsync(file);
            return blob.Uri.AbsoluteUri;
        }
    }
}
