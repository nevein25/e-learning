using e_learning.API.Extensions;
using e_learning.API.Helpers;
using e_learning.Application.CoursesPurshed;
using e_learning.Application.CoursesPurshed.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ICoursesPurshasedService _coursesPurshasedService;

        public EnrollmentController(ICoursesPurshasedService coursesPurshasedService)
        {
            _coursesPurshasedService = coursesPurshasedService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateEnrollment([FromBody]EnrollmentDto enrollmentDto)
        {
            var userId = User.GetUserId(); // Assuming you can get the userId from the authenticated user

            var operationResult = await _coursesPurshasedService.AddOrUpdateEnrollment(enrollmentDto, userId);

            if (operationResult.IsSuccessful)
            {
                return Ok(new { message = operationResult.Message });
            }
            else
            {
                return BadRequest(new { error = operationResult.Message });
            }
        }

        [HttpGet("visited-lessons/{courseId}")]
        public async Task<IActionResult> GetVisitedLessons(int courseId)
        {
            try
            {
                var visitedLessons = await _coursesPurshasedService.GetVisitedLessonsByCourseId(courseId.ToString());
                return Ok(visitedLessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("enrollment/{courseId}")]
        public async Task<IActionResult> GetEnrollment(int courseId)
        {

            var enrollment = await _coursesPurshasedService.GetEnrollmentAsync(courseId, User.GetUserId());

            if (enrollment != null)
            {          
                return Ok(ApiResponse<object>.Success(enrollment));
            }
            else
            {
                return Ok(ApiResponse<int>.Failure("Error"));
            }

        }
    }
}
