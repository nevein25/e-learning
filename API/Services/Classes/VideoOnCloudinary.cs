using API.Services.Interfaces;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Net;

namespace API.Services.Classes
{
    public class VideoOnCloudinary : IVideoService
    {
        private readonly Cloudinary cloudinary;
        public VideoOnCloudinary(Cloudinary _clodinary) => cloudinary = _clodinary;

        public async Task<string?> GetVideo(string publicId)
        {
            string url = cloudinary.Api.UrlVideoUp.BuildUrl($"{publicId}.mp4");
            return (await HttpClinetService.UrlExists(url))?url:null;
        }

        public async Task<UploadResult> Upload(IFormFile file, string filePath)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = filePath,
                    Folder = "courses_videos"
                };
                return await cloudinary.UploadAsync(uploadParams);
            }
        }    
    }
}
