using CloudinaryDotNet.Actions;

namespace API.Services.Interfaces
{
    public interface IUploadService
    {
        public Task<UploadResult> Upload(IFormFile file, string filePath);
    }
}
