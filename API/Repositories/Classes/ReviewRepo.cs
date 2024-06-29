using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public ReviewRepo(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReviewsWithRateingDto>> GetAllReviewsByCourseId(int courseId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.CourseId == courseId)
                .Include(r => r.Student)
                .ThenInclude(r=> r.Rates)
                .ToListAsync();
            var mappedReviews = _mapper.Map<IEnumerable<ReviewsWithRateingDto>>(reviews);

            foreach (var mappedReview in mappedReviews)
            {
                mappedReview.Stars = await _context.Rates.Where(r=>r.CourseId == courseId && r.StudentId == mappedReview.StudentId).Select(r => r.Stars).FirstOrDefaultAsync();   
            }
            return mappedReviews;
        }

        public async Task<Review> AddReviewAsync(ReviewAddDto reviewDto, int studentId) //done
        {
            var review = _mapper.Map<Review>(reviewDto);
            review.StudentId = studentId;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }
        /*
        public async Task<Review> GetReviewById(int id) 
        {
            return await _context.Reviews.FindAsync(id);
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
        */
    }
}
