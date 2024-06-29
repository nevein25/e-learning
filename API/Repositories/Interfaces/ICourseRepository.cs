using API.DTOs;
using API.Entities;
using System.Linq.Expressions;

namespace API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<(IEnumerable<CourseDto>, int)> SearchCoursesAsync(CourseSearchDto searchParams);
        Task<CourseDto> GetCourseById(int Id);

        Task<bool> IfExist(Expression<Func<Course, bool>>predicate);
        Course MapToCourse<T>(T courseDto) where T : class;

        Task<IEnumerable<Course>> GetAllWithInclude();
        Task<bool> Add(Course course);
    }
}
