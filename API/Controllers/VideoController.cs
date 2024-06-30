using API.Context;
using API.DTOs;
using API.Helpers;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVideoService _videoService;

        public VideoController(IUnitOfWork unitOfWork,IVideoService videoService)
        {
            _videoService = videoService;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetLessonVideo")]
        public  IActionResult GetLessonVideo([FromBody]string publicId)
        {
                var videoUrl = _videoService.GetVideo(publicId);
                return (videoUrl.Result!=null)?Ok(ApiResponse<String>.Success(videoUrl.Result))
                                              :Ok(ApiResponse<int>.Failure("Not Found Video....."));
        }
       
        [HttpPost("GetModulesAndLessonCourse")]
        public async Task<IActionResult> GetModulesAndLessonCourse([FromBody]int courseid)
        {
            var courses= await _unitOfWork.CourseRepository.GetAllWithInclude();
            var courseContent = courses.FirstOrDefault(c => c.Id == courseid);

            if (courseContent == null) return Ok(ApiResponse<int>.Failure("Course Not Found"));
               
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
                        id = l.Id,
                        Name = l.Name,
                        Type = l.Type,
                        Content = l.Content,
                        LessonNumber= l.LessonNumber
                    }).ToList()
                }).ToList()
            };
            return Ok(ApiResponse<object>.Success(courseContentDto));
        }
    }
}
