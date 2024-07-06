using e_learning.Domain.Entities;

namespace e_learning.Domain.Repositories
{
    public interface ICategoriesRepository
    {
        public Task<bool> IfCatgoryExist(int catgoryId);
        public Task<IEnumerable<Category>> GetAll();

    }
}
