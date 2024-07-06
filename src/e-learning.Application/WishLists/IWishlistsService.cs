using e_learning.Application.WishLists.DTOs;

namespace e_learning.Application.WishLists
{
    public interface IWishlistsService
    {
        Task<IEnumerable<WishlistDto>> GetWishlist(int userId);
        Task AddToWishlist(WishlistDto wishlistDto, int userId);
        Task<bool> WishlistItemExists(int studentId, int courseId);
        Task<bool> RemoveFromWishlist(int courseId, int UserId);
    }
}
