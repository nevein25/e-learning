using API.Context;
using API.DTOs;
using API.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private EcommerceContext _context { get; }
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService , EcommerceContext context)
        {
            _videoService = videoService;
            _context = context;
        }

        [HttpPost("GetLessonVideo")]
        public  IActionResult GetLessonVideo([FromBody]string publicId)
        {
                var videoUrl = _videoService.GetVideo(publicId);
                return (videoUrl.Result!=null)?Ok(new { link = videoUrl.Result}):NotFound("Video Not Found");
        }
       

        [HttpPost("GetModulesAndLessonCourse")]
        public async Task<IActionResult> GetModulesAndLessonCourse([FromBody]int courseid)
        {
            var courseContent = await _context.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .FirstOrDefaultAsync(c => c.Id == courseid);

            if (courseContent == null)
            {
                return NotFound();
            }

            var courseContentDto = new CourseContentDto
            {
                Name = courseContent.Name,
                Duration = courseContent.Duration,
                Description = courseContent.Description,
                Language = courseContent.Language,
                image = courseContent.Thumbnail,
                Modules = courseContent.Modules.Select(m => new ModuleContentDTO
                {
                    id = m.Id,
                    Name = m.Name,
                    ModuleNumber = m.ModuleNumber,
                    Lessons = m.Lessons.Select(l => new LessonContentDto
                    {
                        Name = l.Name,
                        Type = l.Type,
                        Content = l.Content,
                        LessonNumber= l.LessonNumber
                    }).ToList()
                }).ToList()
            };

            return Ok(courseContentDto);
        }

    }
}
