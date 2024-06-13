using API.Context;
using API.Repositories.Interfaces;

namespace API.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceContext _context;

        public UnitOfWork(EcommerceContext context)
        {
            _context = context;
        }

        public IUserRepo UserRepository => new UserRepo(_context);

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
            // returns true. If ef is tracking any changes to our entities in memory.
        }
    }
}
