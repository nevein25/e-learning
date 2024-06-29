namespace API.Services.Interfaces
{
    public interface IFileService
    {
        public  Task<string> SaveFileAsync(IFormFile file);
    }
}
