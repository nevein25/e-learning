using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Infrastructure.Repositories
{
    internal class CategoriesRepository : ICategoriesRepository
    {
        private readonly ElearningContext _context;
        public CategoriesRepository(ElearningContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAll() => await _context.Categories.ToListAsync();

        public async Task<bool> IfCatgoryExist(int catgoryId) => await _context.Categories.AnyAsync(cat => cat.Id == catgoryId);
    }
}
