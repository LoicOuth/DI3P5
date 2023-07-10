using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using USite.Application.Common.Interfaces;
using USite.Infrastructure.Settings;

namespace USite.Infrastructure.AzureFileStorage
{
    public class AzureFileStorageHelper : IAzureFileStorageHelper
    {
        private readonly IConfiguration _configuration;
        private BlobContainerClient _connection { get; set; }

        public AzureFileStorageHelper(IConfiguration config)
        {
            _configuration = config;
            _connection = new BlobContainerClient(_configuration.GetAzureStorageConnection(), _configuration.GetAzureStorageContainerName());
        }
        public async Task<string> UploadFile(IFormFile myFile, string oldUri)
        {
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(myFile.FileName)}";

            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                await myFile.CopyToAsync(memoryStream);
                bytes = memoryStream.ToArray();
            }
            Stream myBlob = new MemoryStream(bytes);
            var blob = _connection.GetBlobClient(newFileName);

            await blob.UploadAsync(myBlob);

            if (!oldUri.Equals(_configuration.GetDefaultImageUrl()) && oldUri.Contains(_connection.Uri.ToString()))
                await DeleteFileIfExist(oldUri.Split(_connection.Uri.ToString())[1]);

            return $"{_connection.Uri}/{newFileName}";
        }

        public async Task<bool> DeleteFileIfExist(string fileName)
        {
            var blobs = _connection.GetBlobs().Where(b => Path.GetFileNameWithoutExtension(b.Name) == Path.GetFileNameWithoutExtension(fileName));
            if (blobs.Any())
            {
                var result = await _connection.DeleteBlobAsync(blobs.First().Name);

                return result.IsError;
            }

            return false;

        }
    }
}
