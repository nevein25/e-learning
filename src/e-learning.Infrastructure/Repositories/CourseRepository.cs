using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_learning.Infrastructure.Repositories
{
    internal class CourseRepository : ICourseRepository
    {
        private readonly ElearningContext _context;

        public CourseRepository(ElearningContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetTopCoursesWithInstructorsAsync(int number)
        {
            return await _context.Courses.Include(c => c.Instructor).Take(number).ToListAsync();
        }


        public IQueryable<Course> GetQueryableCourses()
        {
            return _context.Courses
                        .Include(c => c.Category)
                        .Include(c => c.Instructor)
                        .AsQueryable();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                                .Include(c => c.Category)
                                .Include(c => c.Instructor)
                                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> IfCourseExistsAsync(Expression<Func<Course, bool>> predicate) => await _context.Courses.AnyAsync(predicate);


        public async Task<bool> AddCourseAsync(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<IEnumerable<Course>> GetAllCoursesData() => await _context.Courses?.Include(c => c.Modules)?.ThenInclude(m => m.Lessons).ToListAsync();

    }
}
