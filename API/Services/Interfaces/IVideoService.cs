using CloudinaryDotNet.Actions;

namespace API.Services.Interfaces
{
    public interface IVideoService
    {
        public Task<UploadResult> Upload(IFormFile file, string filePath);
        public Task<string?> GetVideo(string publicId);
    }
}
