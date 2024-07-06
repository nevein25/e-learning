using e_learning.API.Extensions;
using e_learning.Application.CoursesPurshed;
using e_learning.Application.CoursesPurshed.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesPurshasesController : ControllerBase // did not test
    {
        private readonly ICoursesPurshasedService _coursesPurshasedService;

        public CoursesPurshasesController(ICoursesPurshasedService coursesPurshasedService)
        {
            _coursesPurshasedService = coursesPurshasedService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IList<CoursesPurshasedDto>>> GetCoursesBoughtByStudent()
        {
            var courses = await _coursesPurshasedService.CoursesBoughtByStudentId(User.GetUserId());
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IList<CoursesPurshasedDto>>> IsCourseBoughtAsync(int courseId)
        {
            var isBought = await _coursesPurshasedService.IsCourseBoughtAsync(courseId, User.GetUserId());
            return Ok(new { isBought });
        }

        [Authorize]
        [HttpGet("uploaded")]
        public async Task<ActionResult<IList<CourseUploadedDto>>> GetCoursesUploadedByInstructor()
        {
            var courses = await _coursesPurshasedService.CoursesUploadedByInstructor(User.GetUserId());
            return Ok(courses);
        }

        [HttpGet("finished/{courseId}")]
        public async Task<ActionResult<object>> IsCourseFinishedAsync(int courseId)
        {
            var isFinished = await _coursesPurshasedService.IsStudentFinishedCourseAsync(User.GetUserId(), courseId);
            return Ok(new { isFinished });
        }
    }
}
