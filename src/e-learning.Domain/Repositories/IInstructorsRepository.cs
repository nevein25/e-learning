using e_learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Domain.Repositories
{
    public interface IInstructorsRepository
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int id);
        Task<IEnumerable<Instructor>> GetTopInstructorAsync(int number);
        Task<Instructor> GetInstructorByUserNamesync(string username);
        Task<IEnumerable<Instructor>> GetAllNonVerfiedInstructors();
        Task SaveChangesAsync();

    }
}
