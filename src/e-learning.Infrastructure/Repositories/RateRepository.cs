using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Infrastructure.Repositories
{
    internal class RateRepository : IRatesRepository
    {
        private readonly ElearningContext _context;

        public RateRepository(ElearningContext context)
        {
            _context = context;
        }

        public bool IsCourseExist(int courseId)
        {
            return _context.Courses.Any(p => p.Id == courseId);
        }

        public async Task<Rate> GetRateAsync(int courseId, int userId)
        {
            return await _context.Rates
                .FirstOrDefaultAsync(r => r.CourseId == courseId && r.StudentId == userId);
        }

        public async Task<IEnumerable<Rate>> GetAllRatesByCourseIdAsync(int courseId)
        {
            return await _context.Rates
                .Where(r => r.CourseId == courseId).ToListAsync();
        }
        public async Task AddRateAsync(Rate rate)
        {
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteRateAsync(Rate rate)
        {
            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();
        }
    }
}
