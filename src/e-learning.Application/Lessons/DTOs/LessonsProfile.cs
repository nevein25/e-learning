using AutoMapper;
using e_learning.Application.Modules.DTOs;
using e_learning.Domain.Entities;

namespace e_learning.Application.Lessons.DTOs
{
    internal class LessonsProfile :Profile
    {
        public LessonsProfile()
        {
            CreateMap(typeof(Lesson), typeof(LessonDto)).ReverseMap();
        }
    }
}
