using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface ICatgoryRepository
    {
        public Task<bool> IfCatgoryExist(int catgoryId);
        public Task<IEnumerable<Category>> GetAll();
        
    }
}
