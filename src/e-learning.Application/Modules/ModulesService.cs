using e_learning.Application.Modules.DTOs;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Application.Modules
{
    internal class ModulesService : IModulesService
    {
        private readonly IModulesRepositry _modulesRepositry;
        private readonly ICourseRepository _courseRepository;

        public ModulesService(IModulesRepositry modulesRepositry, ICourseRepository courseRepository)
        {
            _modulesRepositry = modulesRepositry;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<object>> FindModuleByCourseId(int courseId)
        {
            var modules = await _modulesRepositry.FindModule(m => m.CourseId == courseId);

            return modules.Select(m => new 
            {
                Id = m.Id,
                Name = m.Name,
                ModuleNumber = m.ModuleNumber
            });
        }

        public async Task<bool> CreateModuleAsync(ModuleDto moduleDto)
        {
            // Check if Course Exists
            var courseExists = await _courseRepository.IfCourseExistsAsync(c => c.Id == moduleDto.CourseId);
            if (!courseExists)
            {
                return false;
            }

            // Check if Module Name Exists in the Course
            var moduleNameExists = await _modulesRepositry.IfModuleExist(m => m.CourseId == moduleDto.CourseId && m.Name == moduleDto.Name);
            if (moduleNameExists)
            {
                return false;
            }

            // Calculate New Module Number Logic
            var modules = await _modulesRepositry.FindModule(m => m.CourseId == moduleDto.CourseId);
            var moduleNumber = 1 + (modules?.Count() ?? 0);

            // Create Module Entity
            var module = new Module
            {
                CourseId = moduleDto.CourseId,
                Name = moduleDto.Name,
                ModuleNumber = moduleNumber
                // Set other properties as needed
            };

            // Add Module to Repository
            var addedModule = await _modulesRepositry.AddModule(module);
            return addedModule != null;
        }

    }
}
