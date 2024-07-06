
using e_learning.Domain.Common;

namespace e_learning.Domain.Services
{
    public interface IVideoService
    {
        Task<MediaUploadResult?> UploadVideoAsync(byte[] videoData, string filePath);
        Task<string?> GetVideoUrlAsync(string publicId);

    }
}
