using AutoMapper;
using e_learning.Domain.Entities;

namespace e_learning.Application.WishLists.DTOs
{
    internal class WhishlistsProfile : Profile
    {
        public WhishlistsProfile()
        {
            CreateMap<Wishlist, WishlistDto>();
        }
    }
}
