using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Infrastructure.Repositories
{
    internal class ReviewsRepository : IReviewsRepository
    {
        private readonly ElearningContext _context;

        public ReviewsRepository(ElearningContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(int courseId)
        {

               return await _context.Reviews
                    .Where(r => r.CourseId == courseId)
                    .Include(r => r.Student)
                    .ThenInclude(r => r.Rates)
                    .ToListAsync();

        }

        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
