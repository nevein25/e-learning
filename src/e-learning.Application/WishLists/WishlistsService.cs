using AutoMapper;
using e_learning.Application.WishLists.DTOs;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Application.WishLists
{
    internal class WishlistsService : IWishlistsService 
    {
        private readonly IWishlistsRepository _wishlistsRepository;
        private readonly IMapper _mapper;

        public WishlistsService(IWishlistsRepository wishlistsRepository, IMapper mapper)
        {
            _wishlistsRepository = wishlistsRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WishlistDto>> GetWishlist(int userId)
        {
            var wishlistItems = await _wishlistsRepository.GetWishlistByStudentId(userId);

            var wishlistDtos = _mapper.Map<IEnumerable<WishlistDto>>(wishlistItems);

            foreach (var wishlistDto in wishlistDtos)
            {
                var wishlist = wishlistItems.FirstOrDefault(c => c.CourseId == wishlistDto.CourseId);
                if (wishlist != null)
                {
                    wishlistDto.CourseName = wishlist.Course?.Name;
                    wishlistDto.CourseDescription = wishlist.Course?.Description;
                    wishlistDto.Thumbnail = wishlist.Course?.Thumbnail;

                }
            }

            return wishlistDtos;
        }


        public async Task AddToWishlist(WishlistDto wishlistDto, int userId)
        {


            var existingItem = await _wishlistsRepository.GetWishlistItem(userId, wishlistDto.CourseId);

            if (existingItem != null)
            {
                throw new Exception( "Course is already in the wishlist");
            }

            var wishlistItem = new Wishlist
            {
                StudentId = userId,
                CourseId = wishlistDto.CourseId
            };

            await _wishlistsRepository.AddToWishlist(wishlistItem);

       
        }

        public async Task<bool> WishlistItemExists(int studentId, int courseId)
        {
            return await _wishlistsRepository.WishlistItemExists(studentId, courseId);
        }


        public async Task<bool> RemoveFromWishlist(int courseId, int UserId)
        {
     
            var wishlistItem = await _wishlistsRepository.GetWishlistItem(UserId, courseId);

            if (wishlistItem != null)
            {
                await _wishlistsRepository.RemoveFromWishlist(wishlistItem);
                return true;
            }
            return false;
        }

    }
}
