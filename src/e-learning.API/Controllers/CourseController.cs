using Microsoft.AspNetCore.Mvc;
using e_learning.Application.Courses;
using e_learning.Application.Courses.DTOs;
using e_learning.API.Extensions;
using e_learning.API.Helpers;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICoursesService _coursesService;

        public CourseController( ICoursesService coursesService) 
        { 
            _coursesService = coursesService;
        }

        /*
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


        */

        [HttpPost("create-new-course")]
        public async Task<IActionResult> CreateCourseAsync(CourseImgDto courseDto)
        {
            var course = await _coursesService.CreateCourseAsync(courseDto, User.GetUserId());

            if (course != null)
            {
                return Ok(ApiResponse<object>.Success(new { course.Id }));
            }
            else
            {
                return Ok(ApiResponse<int>.Failure("Cant Add Course"));
            }
        }


        [HttpGet("Course/{id}")]
        public async Task<IActionResult> SearchCourseById(int id)
        {
            var course = await _coursesService.GetCourseByIdAsync(id);

            if (course == null)
            {
                Console.WriteLine("No courses found matching the search criteria");
                return NotFound("No courses found matching the search criteria");
            }

            return Ok(course);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchCourses([FromQuery] CourseSearchDto searchParams)
        {
            var (courses, totalCourses) = await _coursesService.SearchCoursesAsync(searchParams);

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

            var courses = await _coursesService.TopCourses(number);
            return Ok(courses);
        }

       

    }
}
