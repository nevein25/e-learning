using e_learning.Domain.Entities;

namespace e_learning.Application.Categories
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
