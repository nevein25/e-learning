using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IUserRepo
    {
        Task<AppUser> GetUserByIdAsync(int id);
        void AddStudent(Student student);
    }
}
