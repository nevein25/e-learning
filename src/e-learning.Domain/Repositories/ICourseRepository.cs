using e_learning.Domain.Entities;
using System.Linq.Expressions;

namespace e_learning.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesData();
        Task<IEnumerable<Course>> GetTopCoursesWithInstructorsAsync(int number);
        IQueryable<Course> GetQueryableCourses();
        Task<Course> GetCourseByIdAsync(int id);
        Task<bool> IfCourseExistsAsync(Expression<Func<Course, bool>> predicate);
        Task<bool> AddCourseAsync(Course course);

    }
}
