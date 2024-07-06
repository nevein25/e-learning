using AutoMapper;
using e_learning.Domain.Entities;

namespace e_learning.Application.CoursesPurshed.DTOs
{
    internal class CoursesPurshaseProfile : Profile
    {
        public CoursesPurshaseProfile()
        {
            CreateMap<Course, CourseUploadedDto>();
            CreateMap<CoursePurchase, EnrollmentDto>().ReverseMap();
        }
    }
}
