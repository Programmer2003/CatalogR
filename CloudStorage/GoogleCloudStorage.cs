using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace CatalogR.CloudStorage
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential credintials;
        private readonly StorageClient storage;
        private readonly string bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            credintials = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            storage = StorageClient.Create(credintials);
            bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
        {
            using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);
            var dataObject = await storage.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
            return dataObject.MediaLink;
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storage.DeleteObjectAsync(bucketName, fileNameForStorage);
        }
    }
}