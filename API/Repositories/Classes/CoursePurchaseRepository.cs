using API.Context;
using API.DTOs;
using API.Entities;
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
                    Thumbnail = c.Thumbnail,
                    Description = c.Description
                })
                .ToListAsync();

            return courses;

        }

        public async Task<bool> IsCourseBoughtAsync(int courseId, int studentId)
        {
            return await _context.CoursePurchases.Where(cp => cp.CourseId == courseId.ToString()
                                                           && cp.UserId == studentId).AnyAsync();
        }

        public async Task<IList<CourseUploadedDto>> CoursesUploadedByInstructor(int instructorId)
        {
            var coursesUploaded = await _context.Courses
                .Where(cp => cp.InstructorId == instructorId)
                .ToListAsync();
   

            var courses =  _mapper.Map<IList<CourseUploadedDto>>(coursesUploaded);


            return courses;

        }
    }
}
