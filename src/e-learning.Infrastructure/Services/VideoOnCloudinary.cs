using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using e_learning.Domain.Common;
using e_learning.Domain.Services;
using e_learning.Infrastructure.Configurations;
using Microsoft.Extensions.Options;


namespace e_learning.Infrastructure.Services
{
    public class VideoOnCloudinary : IVideoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _settings;

        public VideoOnCloudinary(Cloudinary clodinary, IOptions<CloudinarySettings> settings)
        {
            _cloudinary = clodinary;
            _settings = settings.Value;
        }

        public async Task<string?> GetVideoUrlAsync(string publicId)
        {
            string mainPath = $"https://res.cloudinary.com/{_settings.CloudName}/video/upload/";
            //With version
            string url_v1 = $"{mainPath}/v2/{publicId}.mp4";
            //Without Version
            string url_v2 = $"{mainPath}/{publicId}.mp4";
            return (await HttpClinetService.UrlExists(url_v1)) ? url_v1 : 
                (await HttpClinetService.UrlExists(url_v2)) ? url_v2 : null;
        }


        public async Task<MediaUploadResult?> UploadVideoAsync(byte[] videoData, string filePath)
        {
            var uploadParams = new VideoUploadParams
            {
                File = new CloudinaryDotNet.FileDescription(filePath, new MemoryStream(videoData)),
                PublicId = filePath,
                Folder = "courses_videos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            // Map Cloudinary's UploadResult to MediaUploadResult
            return new MediaUploadResult
            {
                Url = uploadResult.Url?.ToString(),
                PublicId = uploadResult.PublicId
            };
        }
    }
}
