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
            string mainPath = "https://res.cloudinary.com/dp9htwhvu/video/upload/";
            //With version
            string url_v1 =$"{mainPath}/v2/{publicId}.mp4";
            //Without Version
            string url_v2 = $"{mainPath}/{publicId}.mp4";
            return (await HttpClinetService.UrlExists(url_v1))? url_v1 : (await HttpClinetService.UrlExists(url_v2))? url_v2:null;
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
