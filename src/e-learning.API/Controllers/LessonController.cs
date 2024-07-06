using e_learning.API.Helpers;
using e_learning.Application.Lessons;
using e_learning.Application.Lessons.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
       
        private readonly ILessonsService _lessonsService;

        public LessonController(ILessonsService lessonsService)
        {
            _lessonsService = lessonsService;
        }


        [HttpPost("create-new-Lesson")]
        public async Task<IActionResult> CreateLessonAsync(LessonDto lessonDto)
        {
            var uploadResult = await _lessonsService.CreateLessonAsync(lessonDto);

            if (uploadResult == null)
                return Ok(ApiResponse<int>.Failure("Failed to create lesson"));

            return Ok(ApiResponse<object>.Success(new { message = "Lesson created successfully", videoPath = uploadResult }));
        }

        [HttpGet("lesson-count/{id}")]
        public async Task<IActionResult> GetLessonCountByCourseId(int id)
        {
            var lessonCount = await _lessonsService.GetLessonCountByCourseId(id);
            return Ok(ApiResponse<int>.Success(lessonCount));
        }
    }
}
