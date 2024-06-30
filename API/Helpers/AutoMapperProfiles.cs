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
            CreateMap<Course, CourseUploadedDto>();




            CreateMap<Course, CourseWithInstructorDto>()
                .ForMember(
                    dest => dest.InstructorName,
                    opt => opt.MapFrom(src => src.Instructor.Name)
                );

            CreateMap<ReviewAddDto, Review>();
            CreateMap<Review, ReviewsWithRateingDto>()
                .ForMember(dest => dest.Username,
                           opt => opt.MapFrom(src => src.Student.UserName))
                
                .ForMember(dest => dest.StudentId,
                           opt => opt.MapFrom(src => src.Student.Id))

                .ForMember(dest => dest.Picture,
                           opt => opt.MapFrom(src => src.Student.Picture));




        }
    }
}
