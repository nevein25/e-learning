using API.DTOs;
using API.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<(IEnumerable<CourseDto>, int)> SearchCoursesAsync(CourseSearchDto searchParams);
        Task<CourseDto> GetCourseById(int Id);

        Task<IEnumerable<CourseWithInstructorDto>> GetTopCourses(int number);
    }
}
