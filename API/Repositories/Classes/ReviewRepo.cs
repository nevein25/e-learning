using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly EcommerceContext _context;

        public ReviewRepo(EcommerceContext context)
        {
            _context = context;
        }
        //public async Task<IEnumerable<ReviewAddDto>> GetAllReviews()
        //{
        //    return (IEnumerable<ReviewAddDto>)await _context.Reviews.ToListAsync();
        //}

        public async Task<Review> GetReviewById(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }
        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChangesAsync();
        }

        public void UpdateReview(Review review)
        {
            if (GetReviewById(review.Id)!=null)
            {
                GetReviewById(review.Id);
                _context.Reviews.Update(review);
                _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Review Not Found");
            }

        }
        public async void DeleteReview(int id)
        {
            Review review = await GetReviewById(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                 _context.SaveChangesAsync();
            }
        }
    }
}
