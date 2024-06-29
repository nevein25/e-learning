using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap(typeof(Course), typeof(CourseImgDto)).ReverseMap();
            CreateMap(typeof(Course), typeof(CourseContentDto)).ReverseMap();
            CreateMap(typeof(Module), typeof(ModuleDto)).ReverseMap();
            CreateMap(typeof(Lesson), typeof(LessonDto)).ReverseMap();
            CreateMap<Course, CourseDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Wishlist, WishlistDto>();
        }
    }
}
