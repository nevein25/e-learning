using Microsoft.AspNetCore.Http;

namespace e_learning.Application.Services.Interfaces
{
    public interface IFileService
    {
        public  Task<string> SaveFileAsync(IFormFile file);
    }
}
