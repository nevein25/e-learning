using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_learning.Infrastructure.Repositories
{
    internal class LessonsRepository : ILessonsRepository
    {
        private readonly ElearningContext _context;

        public LessonsRepository(ElearningContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> FindLesson(Expression<Func<Lesson, bool>> predicate) => await _context.Lessons.Where(predicate).ToListAsync();


        public async Task<bool> AddLesson(Lesson lesson)
        {
            try
            {
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetLessonCountByCourseId(int courseId)
        {
            return await _context.Lessons
                .Include(l => l.Module)
                .CountAsync(l => l.Module.CourseId == courseId);
        }
    }
}
