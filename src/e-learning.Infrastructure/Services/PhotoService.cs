
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using e_learning.Domain.Services;
using e_learning.Domain.Common;


namespace e_learning.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(Cloudinary cloudinary)
        {

            _cloudinary = cloudinary;
        }

        public async Task<MediaUploadResult> AddPhotoAsync(Domain.Common.FileDescription file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Content.Length > 0)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new CloudinaryDotNet.FileDescription(file.FileName, file.Content),
                    Transformation = new Transformation().Height(500).Width(500),
                    Folder = "e-commerce"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return new MediaUploadResult
            {
                Url = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId
            };
        }

        public async Task<bool> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }
    }
}
