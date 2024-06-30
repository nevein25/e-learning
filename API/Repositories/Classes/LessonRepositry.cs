using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace API.Repositories.Classes
{
    public class LessonRepositry : ILessonRepositry
    {

        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public LessonRepositry(EcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Lesson>> Find(Expression<Func<Lesson, bool>> predicate)=> await _context.Lessons.Where(predicate).ToListAsync();

        public Lesson MapToLesson<T>(T lessonDto) where T : class => _mapper.Map<Lesson>(lessonDto);

        public async Task<bool> Add(Lesson lesson)
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
