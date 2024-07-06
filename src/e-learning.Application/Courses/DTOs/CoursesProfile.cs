using AutoMapper;
using e_learning.Domain.Entities;

namespace e_learning.Application.Courses.DTOs
{
    internal class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseWithInstructorDto>()
                .ForMember(
                    dest => dest.InstructorName,
                    opt => opt.MapFrom(src => src.Instructor.Name)
                );

            CreateMap<Course, CourseDto>();

            CreateMap(typeof(Course), typeof(CourseImgDto)).ReverseMap();
            CreateMap(typeof(Course), typeof(CourseContentDto)).ReverseMap();
        }
    }
}
