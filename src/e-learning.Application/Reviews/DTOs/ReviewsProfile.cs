using AutoMapper;
using e_learning.Domain.Entities;

namespace e_learning.Application.Reviews.DTOs
{
    internal class ReviewsProfile : Profile
    {
        public ReviewsProfile()
        {

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
