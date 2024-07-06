using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Infrastructure.Repositories
{
    internal class WishlistsRepository : IWishlistsRepository
    {
        private readonly ElearningContext _context;

        public WishlistsRepository(ElearningContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Wishlist>> GetWishlistByStudentId(int studentId)
        {
            return await _context.Wishlists
                .Where(w => w.StudentId == studentId)
                .Include(w => w.Course)
                .ToListAsync();
        }
        public async Task<Wishlist> GetWishlistItem(int studentId, int courseId)
        {
            return await _context.Wishlists
                .FirstOrDefaultAsync(w => w.StudentId == studentId 
                            && w.CourseId == courseId);
        }

        public async Task AddToWishlist(Wishlist wishlistItem)
        {
            await _context.Wishlists.AddAsync(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> WishlistItemExists(int studentId, int courseId)
        {
            return await _context.Wishlists
                .AnyAsync(w => w.StudentId == studentId && w.CourseId == courseId);
        }

        public async Task RemoveFromWishlist(Wishlist wishlistItem)
        {
            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();
        }
    }
}
