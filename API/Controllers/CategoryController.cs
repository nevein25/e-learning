using API.Context;
using API.Entities;
using API.Helpers;
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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)=> _unitOfWork = unitOfWork;

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _unitOfWork.CatgoryRepository.GetAll();
            return categories.IsNullOrEmpty()?Ok(ApiResponse<object>.Failure("Not Exist Any Catgory..."))
                                             :Ok(ApiResponse<object>.Success(categories));
        }
    }
}
