using API.DTOs;
using API.Entities;
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
        /// If Std pay the Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll()
        {

            try
            {
                var reviews = await _unitOfWork.ReviewRepository.GetAllReviews();
                //if (reviews == null || !reviews.Any())
                //{
                //    return NotFound("No reviews found.");
                //}
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(ReviewAddDto review)
        {

            Review AddReview = new();
            if (review != null)
            {
                AddReview.StudentId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                AddReview.CourseId = review.CourseId;
                AddReview.Text = review.Text;

                _unitOfWork.ReviewRepository.AddReview(AddReview);
                return Ok(review);

            }
            return BadRequest();

        }
        [HttpPut]
        public async Task<ActionResult<Review>> UpdateReview(ReviewUpdateDto review)
        {
            
            Review UpdateReview = new ();
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
    }
}
