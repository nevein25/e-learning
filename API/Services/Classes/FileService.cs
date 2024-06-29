using API.DTOs;
using API.Services.Interfaces;

namespace API.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            // Get the file name and extension
            var fileName = Path.GetFileName(file.FileName);

            // Set the file path where the file will be saved
            string filePath = Path.Combine("uploads", fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
