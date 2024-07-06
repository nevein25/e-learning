using e_learning.API.Helpers;
using e_learning.Application.Modules;
using e_learning.Application.Modules.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModulesService _modulesService;

        public ModuleController(IModulesService modulesService)
        {
            _modulesService = modulesService;
        }

        [HttpPost("create-new-module")]
        public async Task<IActionResult> CreateModuleAsync(ModuleDto moduleDto)
        {
            var isSuccess = await _modulesService.CreateModuleAsync(moduleDto);
            if (isSuccess)
            {
                return Ok(ApiResponse<object>.Success("Module created successfully"));
            }
            else
            {
                return Ok(ApiResponse<int>.Failure("Failed to create module"));
            }
        }



        [HttpPost("GetModulesByCourseId")]
        public async Task<IActionResult> GetModulesByCourseId([FromBody] int courseId)
        {
            var modules = await _modulesService.FindModuleByCourseId(courseId);

            if (modules.IsNullOrEmpty())
            {
                return Ok(ApiResponse<int>.Failure("Course Not Found....."));
            }
            else
            {
                return Ok(ApiResponse<object>.Success(modules));
            }
        }
    }
}
