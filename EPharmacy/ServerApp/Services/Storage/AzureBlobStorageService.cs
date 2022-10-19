using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EPharmacy.ServerApp.Services.Storage
{
    public class AzureBlobStorageService : IAzureBlobStorageService  
    {
        private readonly AzureStorageOptions _options;

        public AzureBlobStorageService(IOptions<AzureStorageOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> UploadFile(string filename, Stream stream)
        {
            var uniqueFileName = Guid.NewGuid().ToString()+ filename;
            var blockBlob = await GetBlockBlobAsync(uniqueFileName);

            stream.Position = 0;
            await blockBlob.UploadFromStreamAsync(stream);
            return blockBlob.Uri.AbsoluteUri;
        }

        private async Task<CloudBlobContainer> GetContainerAsync(string containerName)
        {
            if (CloudStorageAccount.TryParse(_options.ConnectionString, out CloudStorageAccount storageAccount))
            {
                var blobClient = storageAccount.CreateCloudBlobClient();
                var blobContainer = blobClient.GetContainerReference(containerName);
                await blobContainer.CreateIfNotExistsAsync();
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
                return blobContainer;
            }

            return null;
        }

        private async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
        {
            string containerName = "product-images";
            var blobContainer = await GetContainerAsync(containerName);
            return blobContainer.GetBlockBlobReference(blobName);
            
        }
    }
}