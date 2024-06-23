using API.Services.Interfaces;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace API.Services.Classes
{
    public class UploadVideoToCloudinary : IUploadService
    {
        private readonly Cloudinary cloudinary;
        public UploadVideoToCloudinary(Cloudinary _clodinary) => cloudinary = _clodinary;

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
