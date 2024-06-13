using API.Common;
using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("dummy-register")]
        public async Task<IActionResult> DummyRegister()
        {

            var student = new Student
            {
                UserName = "testuser2",
                Email = "test@test.com",
                Name = "Test",
                DOB = new DateOnly(2000, 1, 1),
                Picture = "test.jpg"
            };

            var result = await _userManager.CreateAsync(student, "TEST@test123");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, Roles.Student.ToString());


                return Ok("success");
            }
            else
            {
                return BadRequest($"Failed");
            }
        }

        [HttpPost("dummy-login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return NotFound("User not found");
            

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok("successful");
            

            return BadRequest("error");
        }
    }
}
