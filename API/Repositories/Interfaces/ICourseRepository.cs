using API.DTOs;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<(IEnumerable<CourseDto>, int)> SearchCoursesAsync(CourseSearchDto searchParams);
        Task<CourseDto> GetCourseById(int Id);

    }
}
