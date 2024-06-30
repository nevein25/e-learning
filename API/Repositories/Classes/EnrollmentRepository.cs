using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public EnrollmentRepository(EcommerceContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public CoursePurchase MapToEnrollment<T>(T EnrollmentDto) where T : class => _mapper.Map<CoursePurchase>(EnrollmentDto);
        public async Task<bool> AddEnrollmentAsync(CoursePurchase enrollment)
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
        public async Task<CoursePurchase> GetEnrollmentAsync(int userId, string courseId)
        {
            return await _context.CoursePurchases
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

        public async Task<bool> UpdateEnrollmentAsync(CoursePurchase enrollment)
        {
            _context.CoursePurchases.Update(enrollment);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<int>> GetVisitedLessonsByCourseId(string courseId)
        {
            var enrollment = await _context.CoursePurchases.FirstOrDefaultAsync(e => e.CourseId == courseId);
            if (enrollment != null)
            {
                return enrollment.GetVisitedLessons();
            }
            else
            {
                return new List<int>();
            }
        }
    }
}
