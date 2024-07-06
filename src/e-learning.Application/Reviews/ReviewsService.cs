using AutoMapper;
using e_learning.Application.Reviews.DTOs;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;

namespace e_learning.Application.Reviews
{
    internal class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IRatesRepository _ratesRepository;
        private readonly IMapper _mapper;

        public ReviewsService(IReviewsRepository reviewsRepository,IRatesRepository ratesRepository, IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _ratesRepository = ratesRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReviewsWithRateingDto>> GetReviewsByCourseId(int courseId)
        {

                var reviews = await _reviewsRepository.GetReviewsByCourseIdAsync(courseId);

                var mappedReviews = _mapper.Map<IEnumerable<ReviewsWithRateingDto>>(reviews);

                foreach (var mappedReview in mappedReviews)
                {
                    mappedReview.Stars = (await _ratesRepository.GetRateAsync(courseId, mappedReview.StudentId))?.Stars;
                }
                return mappedReviews;
            
        }

        public async Task AddReview(ReviewAddDto reviewDto, int studentId)
        {
            //    var isCourseBought = await _unitOfWork.CoursePurchaseRepository.IsCourseBoughtAsync(reviewDto.CourseId, User.GetUserId());
            //    //  if (!isCourseBought) return BadRequest("You Can not review course you did not buy");
            var review = _mapper.Map<Review>(reviewDto);
            review.StudentId = studentId;
            await _reviewsRepository.AddReviewAsync(review);
        }
    }
}
