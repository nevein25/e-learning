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

        public string GetVideo(string publicId)
        {
            return  cloudinary.Api.UrlVideoUp.BuildUrl($"{publicId}.mp4");
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
