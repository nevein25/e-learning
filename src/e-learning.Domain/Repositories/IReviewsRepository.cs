using e_learning.Domain.Entities;

namespace e_learning.Domain.Repositories
{
    public interface IReviewsRepository
    {
        Task<IEnumerable<Review>> GetReviewsByCourseIdAsync(int courseId);
        Task AddReviewAsync(Review review);
    }
}
