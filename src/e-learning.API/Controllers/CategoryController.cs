using e_learning.API.Helpers;
using e_learning.Application.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace e_learning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoriesService.GetAll();
            return categories.IsNullOrEmpty()?Ok(ApiResponse<object>.Failure("Not Exist Any Catgory..."))
                                             :Ok(ApiResponse<object>.Success(categories));
        }
    }
}
