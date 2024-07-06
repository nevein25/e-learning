using e_learning.API.Helpers;
using e_learning.Application.Lessons;
using e_learning.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly ILessonsService _lessonsService;

        public VideoController(IVideoService videoService, ILessonsService lessonsService)
        {
            _videoService = videoService;
            _lessonsService = lessonsService;
        }


        [HttpPost("GetLessonVideo")]
        public async Task<IActionResult> GetLessonVideo([FromBody]string publicId)
        {
                var videoUrl = await _videoService.GetVideoUrlAsync(publicId);
                return (videoUrl!=null)?Ok(ApiResponse<String>.Success(videoUrl))
                                              :Ok(ApiResponse<int>.Failure("Not Found Video....."));
        }
       
        [HttpPost("GetModulesAndLessonCourse")]
        public async Task<IActionResult> GetModulesAndLessonCourse([FromBody]int courseid)
        {
            var courseContent = await _lessonsService.GetCourseWithLessonsContent(courseid);

            if (courseContent == null) return Ok(ApiResponse<int>.Failure("Course Not Found"));
               
            return Ok(ApiResponse<object>.Success(courseContent));
        }
    }
}
