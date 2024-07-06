using e_learning.Domain.Common;

namespace e_learning.Domain.Services
{
    public interface IPhotoService
    {
        Task<MediaUploadResult> AddPhotoAsync(FileDescription file);
        Task<bool> DeletePhotoAsync(string publicId);
    }
}
