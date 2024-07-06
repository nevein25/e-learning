using AutoMapper;
using e_learning.Domain.Entities;

namespace e_learning.Application.Instructors.DTOs
{
    internal class InstructorsProfile : Profile
    {
        public InstructorsProfile()
        {
            CreateMap<Instructor, InstructorDto>();
        }
    }
}
