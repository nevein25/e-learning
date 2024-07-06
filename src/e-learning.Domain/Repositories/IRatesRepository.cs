using e_learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Domain.Repositories
{
    public interface IRatesRepository
    {
        public bool IsCourseExist(int courseId);
        Task<Rate> GetRateAsync(int courseId, int userId);
        Task<IEnumerable<Rate>> GetAllRatesByCourseIdAsync(int courseId);
        Task AddRateAsync(Rate rate);
       Task DeleteRateAsync(Rate rate);
    }
}
