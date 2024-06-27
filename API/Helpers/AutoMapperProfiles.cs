using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Wishlist, WishlistDto>();

          
                    
            CreateMap<Course, CourseWithInstructorDto>()
                .ForMember(
                    dest => dest.InstructorName,
                    opt => opt.MapFrom(src => src.Instructor.Name)
                );
            

        }
    }
}
