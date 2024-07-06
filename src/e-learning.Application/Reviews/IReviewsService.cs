using e_learning.Application.Reviews.DTOs;

namespace e_learning.Application.Reviews
{
    public interface IReviewsService
    {

        Task<IEnumerable<ReviewsWithRateingDto>> GetReviewsByCourseId(int courseId);
        Task AddReview(ReviewAddDto reviewDto, int studentId);
    }
}
