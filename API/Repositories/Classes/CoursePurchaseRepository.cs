using API.Context;
using API.DTOs;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class CoursePurchaseRepository : ICoursePurchaseRepository
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public CoursePurchaseRepository(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<CoursesPurshasedDto>> CoursesBoughtByStudentId(int studentId)
        {
            var coursePurchases = await _context.CoursePurchases
                .Where(cp => cp.UserId == studentId)
                .ToListAsync();

            var courseIds = coursePurchases
                .Select(cp => int.TryParse(cp.CourseId, out var id) ? (int?)id : null)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            var courses = await _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .Select(c => new CoursesPurshasedDto
                {
                    CourseId = c.Id,
                    CourseName = c.Name,
                    Thumbnail = c.Thumbnail
                })
                .ToListAsync();

            return courses;

        }

    }
}
