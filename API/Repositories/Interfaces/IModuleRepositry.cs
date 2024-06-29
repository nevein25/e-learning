using API.Entities;
using System.Linq.Expressions;

namespace API.Repositories.Interfaces
{
    public interface IModuleRepositry
    {
        Task<bool> IfExist(Expression<Func<Module, bool>> predicate);
        Task<IEnumerable<Module>> Find(Expression<Func<Module, bool>> predicate);
        Module MapToModule<T>(T moduleDto) where T : class;
        Task<bool> Add(Module module);
        Task<Module?> FindFirst(Expression<Func<Module, bool>> predicate);
    }
}