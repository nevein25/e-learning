using API.Context;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
       
        private readonly IVideoService _videoService;
        private readonly IUnitOfWork _unitOfWork;

        public LessonController(IVideoService videoService, IUnitOfWork unitOfWork)
        {
            _videoService = videoService;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("create-new-Lesson")]
        public async Task<IActionResult> CreateLessonAsync(LessonDto lessonDto)
        {
            //Check Module Found Or Not
            var module = await _unitOfWork.ModuleRepositry.FindFirst(m => m.Id == lessonDto.ModuleId);
            if (module == null) return Ok(ApiResponse<int>.Failure("Module not found"));


            //New Lesson Number Logic
            var Lessons = await _unitOfWork.LessonRepositry.Find(lesson => lesson.ModuleId == lessonDto.ModuleId);
            var newLessonNumber = 1 + Lessons?.Count() ?? 0;
            var filePath = $"{module.Course.Name}/Chapter_{module.ModuleNumber}/Lesson_{newLessonNumber}";

            //Upload To Cloudinary
            var uploadResult = await _videoService.Upload(lessonDto.VideoContent, filePath);
            if (uploadResult == null) return Ok(ApiResponse<int>.Failure("File upload failed"));


            //Create Lesson 
            var lesson = _unitOfWork.LessonRepositry.MapToLesson(lessonDto);
            lesson.LessonNumber = newLessonNumber;

            //Save To DB.
            return (await _unitOfWork.LessonRepositry.Add(lesson))?Ok(ApiResponse<object>.Success(new { message = "Added Successfully", videoPath = uploadResult }))
                                                                  :Ok(ApiResponse<int>.Failure("Cant Upload Now Try Again")); ;
        }
    }
}
