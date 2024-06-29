using API.Context;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Repositories.Classes;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModuleController(IUnitOfWork unitOfWork)=> _unitOfWork = unitOfWork;

        [HttpPost("create-new-module")]
        public async Task<IActionResult> CreateModuleAsync(ModuleDto moduleDto)
        {
            //Check if Course Exist 
            var courseExists = await _unitOfWork.CourseRepository.IfExist(c => c.Id == moduleDto.CourseId);
            if (!courseExists) return Ok(ApiResponse<int>.Failure("Course not found"));
            

            //Check the ModuleName Exist Already in that Course
            var ModuleNameExists = await _unitOfWork.ModuleRepositry.IfExist(l => l.CourseId == moduleDto.CourseId && l.Name == moduleDto.Name);
            if (ModuleNameExists) return Ok(ApiResponse<int>.Failure("Module name already exists!")); 
            

            //New ModuleNumber Logic
            var module = _unitOfWork.ModuleRepositry.MapToModule(moduleDto);
            var modules = await (_unitOfWork.ModuleRepositry.Find(module => module.CourseId == moduleDto.CourseId));
            module.ModuleNumber = 1+modules?.Count()??0;

            //Add To DB
            return (await _unitOfWork.ModuleRepositry.Add(module))? Ok(ApiResponse<object>.Success(module.Name)): Ok(ApiResponse<int>.Failure("Cant add Now Try Again")); ;
        }



        [HttpPost("GetModulesByCourseId")]
        public async Task<IActionResult> GetModulesByCourseId([FromBody]int courseId)
        {
            var modules = await _unitOfWork.ModuleRepositry.Find(m => m.CourseId == courseId);
            return (modules.IsNullOrEmpty())?Ok(ApiResponse<int>.Failure("Course Not Found.....")):
                                             Ok(ApiResponse<object>.Success(modules.Select(m => new{Id = m.Id,Name = m.Name,ModuleNumber = m.ModuleNumber})));
        }
    }
}
