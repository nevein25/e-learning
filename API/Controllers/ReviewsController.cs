using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("{courseId}")]
        public async Task<ActionResult<IEnumerable<ReviewsWithRateingDto>>> GetReviewsByCourseId(int courseId)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetAllReviewsByCourseId(courseId);
            return Ok(reviews);

        }


        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(ReviewAddDto reviewDto)
        {
            var isCourseBought = await _unitOfWork.CoursePurchaseRepository.IsCourseBoughtAsync(reviewDto.CourseId, User.GetUserId());
          //  if (!isCourseBought) return BadRequest("You Can not review course you did not buy");
            var createdReview = await _unitOfWork.ReviewRepository.AddReviewAsync(reviewDto, User.GetUserId());
            return Ok();
        }
        /*
        [HttpPut]
        public async Task<ActionResult<Review>> UpdateReview(ReviewUpdateDto review)
        {

            Review UpdateReview = new();
            if (review != null)
            {
                UpdateReview.StudentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                UpdateReview.CourseId = review.CourseId;
                UpdateReview.Text = review.Text;

                _unitOfWork.ReviewRepository.UpdateReview(UpdateReview);
                return Ok(review);
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<ActionResult<Review>> DelelteReview(ReviewUpdateDto review)
        {
            if (review != null)
            {
                _unitOfWork.ReviewRepository.DeleteReview(review.Id);
                return NoContent();

            }
            return BadRequest();
        }
        */
    }
}
