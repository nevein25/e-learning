using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Infrastructure.Repositories
{
    internal class CoursesPurshasedRepository : ICoursesPurshasedRepository
    {
        private readonly ElearningContext _context;

        public CoursesPurshasedRepository(ElearningContext context)
        {
            _context = context;
        }

        public async Task<IList<CoursePurchase>> GetCoursePurchasesByStudentIdAsync(int studentId)
        {
            return await _context.CoursePurchases.Where(cp => cp.UserId == studentId).ToListAsync();
        }

        public async Task<IList<Course>> GetCoursesByIdsAsync(List<int> courseIds)
        {
            return await _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<bool> IsCourseBoughtExisitAsync(int courseId, int studentId)
        {
            return await _context.CoursePurchases.Where(cp => cp.CourseId == courseId.ToString()
                                                           && cp.UserId == studentId).AnyAsync();
        }

        public async Task<CoursePurchase> GetCoursePurchaseAsync(int studentId, int courseId)
        {
            return await _context.CoursePurchases
                .FirstOrDefaultAsync(cp => cp.UserId == studentId && cp.CourseId == courseId.ToString());
        }

        public async Task<IList<Course>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            return await _context.Courses
                .Where(c => c.InstructorId == instructorId)
                .ToListAsync();
        }
        public async Task<IList<CoursePurchase>> GetCoursePurchasesByCourseIdAsync(string courseId)
        {
            return await _context.CoursePurchases
                .Where(cp => cp.CourseId == courseId.ToString())
                .ToListAsync();
        }
        public async Task CreateCoursePurchaseAsync(CoursePurchase coursePurchase)
        {
            _context.CoursePurchases.Add(coursePurchase);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> UpdateCoursePurchaseAsync(CoursePurchase enrollment)
        {
            _context.CoursePurchases.Update(enrollment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddCoursePurchaseAsync(CoursePurchase enrollment)
        {
            try
            {
                _context.CoursePurchases.Add(enrollment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CoursePurchase> GetCoursePurchasesByCourseId(string courseId)
        {
           return await _context.CoursePurchases.FirstOrDefaultAsync(e => e.CourseId == courseId);
        }
    }
}
