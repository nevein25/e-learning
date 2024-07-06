using e_learning.Application.Courses.DTOs;
using e_learning.Domain.Entities;

namespace e_learning.Application.Courses
{
    public interface ICoursesService
    {
        Task<IEnumerable<CourseWithInstructorDto>> TopCourses(int number);
        Task<(IEnumerable<CourseDto>, int)> SearchCoursesAsync(CourseSearchDto searchParams);
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task<Course> CreateCourseAsync(CourseImgDto courseDto, int instructorId);
    }
}
