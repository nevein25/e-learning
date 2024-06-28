using API.Context;
using API.DTOs;
using API.Extensions;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesPurshasesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesPurshasesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IList<CoursesPurshasedDto>>> GetCoursesBoughtByStudent()
        {
            var courses = await _unitOfWork.CoursePurchaseRepository.CoursesBoughtByStudentId(User.GetUserId());
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<IList<CoursesPurshasedDto>>> IsCourseBoughtAsync(int courseId)
        {
            var isBought = await _unitOfWork.CoursePurchaseRepository.IsCourseBoughtAsync(courseId, User.GetUserId());
            return Ok(new { isBought });
        }
    }
}
