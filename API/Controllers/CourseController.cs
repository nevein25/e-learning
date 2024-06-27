using API.Context;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Repositories.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private EcommerceContext _context { get; }
        private readonly IVideoService _videoService;
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IVideoService videoService , EcommerceContext context , IUnitOfWork unitOfWork) 
        { 
            _videoService = videoService;
            _context = context;
             _unitOfWork = unitOfWork;
        }


        [HttpPost("create-new-course")]
        public async Task<IActionResult> CreateCourseAsync(CourseImgDto courseDto)
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

            // Get the file name and extension
            var fileName = Path.GetFileName(courseDto.Thumbnail.FileName);

            // Set the file path where the file will be saved
            string filePath = Path.Combine("uploads", fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await courseDto.Thumbnail.CopyToAsync(stream);
            }

            // Store only the file name and extension in the database
            var course = new Course
            {
                Name = courseDto.Name,
                Duration = courseDto.Duration,
                Description = courseDto.Description,
                Price = courseDto.Price,
                Language = courseDto.Language,
                Thumbnail = fileName, // Save only the file name and extension
                InstructorId = courseDto.InstructorId,
                CategoryId = courseDto.CategoryId,
                UploadDate = DateTime.UtcNow
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        [HttpGet("Course/{id}")]
        public async Task<IActionResult> SearchCourseById(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseById(id);
            if (course is null)
            {
                Console.WriteLine("No courses found matching the search criteria");
                return NotFound("No courses found matching the search criteria");
            }

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
                ModuleNumber = moduleDto.Id,
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

            var highestLessonNumber = _context.Lessons
            .OrderByDescending(l => l.LessonNumber)
            .FirstOrDefault()?.LessonNumber ?? 0;

       

            var newLesson = new Lesson
            {
                Name = lessonDto.Name,
                Type = lessonDto.Type,
                Content = lessonDto.Content,
                LessonNumber = highestLessonNumber + 1,
                ModuleId = lessonDto.ModuleId,
            };

            _context.Lessons.Add(newLesson);
            await _context.SaveChangesAsync();
            var filePath = $"{module.Course.Name}/Chapter_{lessonDto.ModuleId}/Lesson_{highestLessonNumber+1}";
            //string videoFilePath = "C:\\Users\\pc\\Downloads\\SQL.mp4";

            //Here Upload the Video to the Cloudinary.
            //IFormFile mockVideoFile = MoqIFormFile.CreateMockFormFile(videoFilePath);
            var uploadResult = await _videoService.Upload(lessonDto.VideoContent, filePath);
            if (uploadResult == null) return BadRequest("File upload failed");

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
                .Select(course => new CourseImgDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Duration = course.Duration,
                    Description = course.Description,
                    Price = course.Price,
                    Language = course.Language,
                    img = course.Thumbnail,
                    InstructorId = course.InstructorId,
                    CategoryId = course.CategoryId,
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchCourses([FromQuery] CourseSearchDto searchParams)
        {
            Console.WriteLine($"Received search request with parameters: Name={searchParams.Name}, MinPrice={searchParams.MinPrice}, MaxPrice={searchParams.MaxPrice}, CategoryId={searchParams.CategoryId}, PageNumber={searchParams.PageNumber}, PageSize={searchParams.PageSize}");

            var (courses, totalCourses) = await _unitOfWork.CourseRepository.SearchCoursesAsync(searchParams);

            if (!courses.Any())
            {
                Console.WriteLine("No courses found matching the search criteria");
                return NotFound("No courses found matching the search criteria");
            }

            return Ok(new { Courses = courses, TotalCourses = totalCourses });
        }

        
    }
}
