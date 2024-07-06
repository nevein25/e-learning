using e_learning.API.Extensions;
using e_learning.Application.WishLists;
using e_learning.Application.WishLists.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase 
    {
        private readonly IWishlistsService _wishlistsService;

        public WishlistController(IWishlistsService wishlistsService)
        {
            _wishlistsService = wishlistsService;
        }
        // GET: api/wishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetWishlist()
        {
            var wishlist = await _wishlistsService.GetWishlist(User.GetUserId());
            return Ok(wishlist);
        }

        // POST: api/wishlist/add
        [HttpPost("add")]
        public async Task<ActionResult> AddToWishlist(WishlistDto wishlistDto)
        {

            try
            {
                await _wishlistsService.AddToWishlist(wishlistDto, User.GetUserId());
                return Ok();
            }
            catch
            {
                return Conflict(new { message = "Course is already in the wishlist" });

            }
        

        }

        // DELETE: api/wishlist/{courseId}
        [HttpDelete("{courseId}")]
        public async Task<ActionResult> RemoveFromWishlist(int courseId)
        {
            var userId = User.GetUserId();

            var isRemoved = await _wishlistsService.RemoveFromWishlist(courseId, userId);

            if (!isRemoved)
            {
                return NotFound(new { message = "Course not found in the wishlist" });
            }

            return Ok(new { message = "Course removed from wishlist" });
        }

        [HttpGet("exit-wishlist/{courseId}")]
        public async Task<ActionResult<bool>> CheckCourseExsistanceInWishlist(int courseId)
        {
            return Ok(await _wishlistsService.WishlistItemExists(User.GetUserId(), courseId));
        }
    }
}
