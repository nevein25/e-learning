using API.Context;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Classes
{
    public class CatgoryRepository: ICatgoryRepository
    {

        private readonly EcommerceContext _context;
        public CatgoryRepository(EcommerceContext context)=> _context = context;

        public async Task<IEnumerable<Category>> GetAll()=>await _context.Categories.ToListAsync();

        public async Task<bool> IfCatgoryExist(int catgoryId)=> await _context.Categories.AnyAsync(cat => cat.Id == catgoryId);
        
    }
}
