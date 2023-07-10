using Microsoft.AspNetCore.Http;

namespace USite.Application.Common.Interfaces
{
    public interface IAzureFileStorageHelper
    {
        Task<string> UploadFile(IFormFile myFile, string oldUri);
        Task<bool> DeleteFileIfExist(string fileName);
    }
}
