using CloudinaryDotNet.Actions;

namespace API.Services.Interfaces
{
    public interface IVideoService
    {
        Task<UploadResult> Upload(IFormFile file, string filePath);
        string GetVideo(string path);
    }
}
