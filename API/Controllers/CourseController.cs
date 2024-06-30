using API.Context;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Repositories.Interfaces;
using API.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;


        public CourseController(IFileService fileService,IUnitOfWork unitOfWork) 
        { 
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("create-new-course")]
        public async Task<IActionResult> CreateCourseAsync(CourseImgDto courseDto)
        {
            //Check Catgory Exist.....
            var categoryExists = await _unitOfWork.CatgoryRepository.IfCatgoryExist(courseDto.CategoryId);
            if (!categoryExists) return Ok(ApiResponse<int>.Failure("Catgory Not Exist"));


            //Check Course Name Exist Or NOt....
            var courseExists = await _unitOfWork.CourseRepository.IfExist(c=>c.Name==courseDto.Name);
            if (courseExists) return Ok(ApiResponse<int>.Failure("Course Name Already Exist"));


            //Using Mapper......
            var course = _unitOfWork.CourseRepository.MapToCourse(courseDto);
            course.Thumbnail = (await _fileService.SaveFileAsync(courseDto.Thumbnail));
            course.InstructorId = User.GetUserId();
            course.UploadDate = DateTime.UtcNow;
            
            return   (await _unitOfWork.CourseRepository.Add(course)?Ok(ApiResponse<object>.Success(new { course.Id }))
                                                                     :Ok(ApiResponse<int>.Failure("Cant Add Course")));
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

        [HttpGet("top-courses/{number}")]
        public async Task<IActionResult> TopCourses(int number)
        {

            var courses = await _unitOfWork.CourseRepository.GetTopCourses(number);
            return Ok(courses);
        }

        
        [HttpGet("avg-rate-courses/{courseId}")]
        public async Task<ActionResult<object>> AvgCourseRate(int courseId)
        {

            var avgRating = await _unitOfWork.RateRepository.GetAvgRateForCourseAsync(courseId);
            return Ok(new {avgRating});
        }


    }
}
