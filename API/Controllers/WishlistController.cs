using API.Context;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly EcommerceContext _context; // Replace with your DbContext
        private readonly IMapper _mapper;

        public WishlistController(EcommerceContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/wishlist
        [HttpGet]
        public async Task<ActionResult> GetWishlist()
        {
            var wishlistItems = await _context.Wishlists
                .Where(s => s.StudentId == User.GetUserId())
                .Include(w => w.Student)
                .Include(w => w.Course)
                .ToListAsync();

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

            return Ok(wishlistDtos);
        }

        // POST: api/wishlist/add
        [HttpPost("add")]
        public async Task<ActionResult> AddToWishlist(WishlistDto wishlistDto)
        {
            var userId = User.GetUserId();

            var existingItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.StudentId == userId && w.CourseId == wishlistDto.CourseId);

            if (existingItem != null)
            {
                return Conflict(new { message = "Course is already in the wishlist" });
            }

            var wishlistItem = new Wishlist
            {
                StudentId = userId,
                CourseId = wishlistDto.CourseId
            };

            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();

            return Ok(wishlistItem);
        }

        // DELETE: api/wishlist/{courseId}
        [HttpDelete("{courseId}")]
        public async Task<ActionResult> RemoveFromWishlist(int courseId)
        {
            var userId = User.GetUserId();

            var wishlistItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.StudentId == userId && w.CourseId == courseId);

            if (wishlistItem == null)
            {
                return NotFound(new { message = "Course not found in the wishlist" });
            }

            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course removed from wishlist" });
        }

    }
}
