using e_learning.API.Extensions;
using e_learning.Application.Reviews;
using e_learning.Application.Reviews.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<ReviewsWithRateingDto>>> GetReviewsByCourseId(int courseId)
        {
            var reviews = await _reviewsService.GetReviewsByCourseId(courseId);
            return Ok(reviews);
        }


        [HttpPost]
        public async Task<ActionResult> AddReview(ReviewAddDto reviewDto)
        {
           // var isCourseBought = await _unitOfWork.CoursePurchaseRepository.IsCourseBoughtAsync(reviewDto.CourseId, User.GetUserId());
             //  if (!isCourseBought) return BadRequest("You Can not review course you did not buy");
             await _reviewsService.AddReview(reviewDto, User.GetUserId());
            return Ok();
        }

    }
}
