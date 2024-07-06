using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Infrastructure.Repositories
{
    internal class InstructorsRepository : IInstructorsRepository
    {
        private readonly ElearningContext _context;

        public InstructorsRepository(ElearningContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            return await _context.Instructors.ToListAsync();

        }
        public async Task<Instructor> GetInstructorByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public async Task<IEnumerable<Instructor>> GetTopInstructorAsync(int number)
        {
           return await _context.Instructors.Take(number).ToListAsync();
        }
        public async Task<Instructor> GetInstructorByUserNamesync(string username)
        {
            return await _context.Instructors.FirstOrDefaultAsync(i => i.UserName == username);
        }

        public async Task<IEnumerable<Instructor>> GetAllNonVerfiedInstructors()
        {
           return await _context.Instructors.Where(i => i.IsVerified == false).ToListAsync();

        }
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
