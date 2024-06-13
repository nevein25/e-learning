using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class UserRepo : IUserRepo
    {
        private readonly EcommerceContext _context;

        public UserRepo(EcommerceContext context)
        {
            _context = context;
        }

        // we will noot need it, we can use identity instead
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);

        }


    }
}
