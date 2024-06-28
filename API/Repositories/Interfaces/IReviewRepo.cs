using API.DTOs;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        Task<Review> AddReviewAsync(ReviewAddDto reviewDto, int studentId);
        Task<IEnumerable<ReviewsWithRateingDto>> GetAllReviewsByCourseId(int courseId);

        /*
        Task<Review> GetReviewById(int id);
        void UpdateReview(Review review);
        void DeleteReview(int id);
        */
    }
}
