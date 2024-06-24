using API.DTOs;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<(IEnumerable<Course>, int)> SearchCoursesAsync(CourseSearchDto searchParams);
        Task<CourseDto> GetCourseById(int Id);

    }
}
