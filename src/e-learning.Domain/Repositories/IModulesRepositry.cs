using e_learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_learning.Domain.Repositories
{
    public interface IModulesRepositry
    {
        Task<IEnumerable<Module>> FindModule(Expression<Func<Module, bool>> predicate);

        Task<Module?> FindFirstModule(Expression<Func<Module, bool>> predicate);


        Task<bool> IfModuleExist(Expression<Func<Module, bool>> predicate);
        Task<bool> AddModule(Module module);
    }
}
