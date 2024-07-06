using e_learning.Domain.Entities;

namespace e_learning.Domain.Repositories
{
    public interface IWishlistsRepository
    {
        Task<IEnumerable<Wishlist>> GetWishlistByStudentId(int studentId);
        Task<Wishlist> GetWishlistItem(int studentId, int courseId);
        Task AddToWishlist(Wishlist wishlistItem);
        Task<bool> WishlistItemExists(int studentId, int courseId);
        Task RemoveFromWishlist(Wishlist wishlistItem);
    }
}
