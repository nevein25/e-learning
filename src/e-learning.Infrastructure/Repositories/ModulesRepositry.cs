using API.Infrastructure.Context;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_learning.Infrastructure.Repositories
{
    internal class ModulesRepositry : IModulesRepositry
    {
        private readonly ElearningContext _context;

        public ModulesRepositry(ElearningContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> FindModule(Expression<Func<Module, bool>> predicate) => await _context.Modules.Where(predicate).ToListAsync();

        public async Task<Module?> FindFirstModule(Expression<Func<Module, bool>> predicate) => await _context.Modules.Include(m => m.Course).FirstOrDefaultAsync(predicate);


        public async Task<bool> IfModuleExist(Expression<Func<Module, bool>> predicate) => await _context.Modules.AnyAsync(predicate);

        public async Task<bool> AddModule(Module module)
        {
            try
            {
                _context.Modules.Add(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
