using API.Context;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private EcommerceContext _context { get; }
        private readonly IUploadService _uploadService;
        public CourseController(IUploadService uploadService , EcommerceContext context) 
        { 
            _uploadService = uploadService;
            _context = context;
        }


        [HttpPost("create-new-course")]
        public async Task<IActionResult> CreateCourseAsync(CourseDto courseDto)
        {
            var instructorExists = await _context.Instructors.AnyAsync(i => i.Id == courseDto.InstructorId);
            if (!instructorExists)
            {
                return NotFound("Instructor not found");
            }
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == courseDto.CategoryId);
            if (!categoryExists)
            {
                return NotFound("Category not found");
            }

            var course = new Course
            {
                Name = courseDto.Name,
                Duration = courseDto.Duration,
                Description = courseDto.Description,
                Price = courseDto.Price,
                Language = courseDto.Language,
                Thumbnail = courseDto.Thumbnail,
                InstructorId = courseDto.InstructorId,
                CategoryId = courseDto.CategoryId,
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpPost("create-new-module")]
        public async Task<IActionResult> CreateModuleAsync(ModuleDto moduleDto)
        {
            var courseExists = await _context.Courses.AnyAsync(c => c.Id == moduleDto.CourseId);
            if (!courseExists)
            {
                return NotFound("Course not found");
            }
            // in module 
            var ModuleNameExists = await _context.Modules.AnyAsync(l => l.Name == moduleDto.Name);
            if (ModuleNameExists)
            {
                return BadRequest("Module name already exists!");
            }
            var module = new Module
            {
                Name = moduleDto.Name,
                ModuleNumber = moduleDto.ModuleNumber,
                CourseId = moduleDto.CourseId
            };

            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            return Ok(module);
        }

        [HttpPost("create-new-Lesson")]
        public async Task<IActionResult> CreateLessonAsync(LessonDto lessonDto)
        {
            var module = await _context.Modules.Include(m => m.Course).FirstOrDefaultAsync(m => m.Id == lessonDto.ModuleId);

            if (module == null)
            {
                return NotFound("Module not found");
            }

            //For Test Only
            var filePath = $"{module.Course.Name}/Chapter_{lessonDto.ModuleId}/Lesson_{lessonDto.LessonNumber}";
            //string videoFilePath = "C:\\Users\\pc\\Downloads\\SQL.mp4";

            //Here Upload the Video to the Cloudinary.
            //IFormFile mockVideoFile = MoqIFormFile.CreateMockFormFile(videoFilePath);
            var uploadResult = await _uploadService.Upload(lessonDto.VideoContent, filePath);
            if (uploadResult == null) return BadRequest("File upload failed");

            var newLesson = new Lesson
            {
                Name = lessonDto.Name,
                Type = lessonDto.Type,     
                Content = lessonDto.Content,
                LessonNumber = lessonDto.LessonNumber,
                ModuleId = lessonDto.ModuleId
            };

            _context.Lessons.Add(newLesson);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Added Successfully", videoPath = uploadResult });
        }

        [HttpGet("GetAllInstructors")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var instructors = await _context.Instructors.ToListAsync();
            return Ok(instructors);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _context.Courses
                .Select(course => new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Duration = course.Duration,
                    Description = course.Description,
                    Price = course.Price,
                    Language = course.Language,
                    Thumbnail = course.Thumbnail,
                    InstructorId = course.InstructorId,
                    CategoryId = course.CategoryId,
                    Modules = course.Modules.Select(m => m.Name).ToList()
                })
                .ToListAsync();

            return Ok(courses);
        }

        [HttpGet("GetAllModules")]
            public async Task<IActionResult> GetAllModules()
            {
                var modules = await _context.Modules
                    .Select(m => new ModuleDto
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ModuleNumber = m.ModuleNumber,
                        CourseId = m.CourseId
                    })
                    .ToListAsync();

                return Ok(modules);
            }

    }
}
