using API.DTOs;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IReviewRepo
    {
        Task<IEnumerable<Review>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int id);
    }
}
