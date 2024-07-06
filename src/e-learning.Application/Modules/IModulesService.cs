using e_learning.Application.Modules.DTOs;
using e_learning.Domain.Entities;

namespace e_learning.Application.Modules
{
    public interface IModulesService
    {
        Task<IEnumerable<object>> FindModuleByCourseId(int courseId);
        Task<bool> CreateModuleAsync(ModuleDto moduleDto);
    }
}
