
using e_learning.Application.DTOs.Auth;
using e_learning.Application.Instructors;
using e_learning.Domain.Constans;
using e_learning.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace e_learning.Application.Auth
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IInstructorsService _instructorsService;

        public AuthService(UserManager<AppUser> userManager,
                            ITokenService tokenService,
                            IInstructorsService instructorsService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _instructorsService = instructorsService;
        }
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            bool isInstructorVerified = false;
            AppUser user = await _userManager.Users.SingleOrDefaultAsync(
                x => x.Email == loginDto.EmailOrUsername || x.UserName == loginDto.EmailOrUsername
            );


            if (user == null)  throw new Exception("Invalid username or password");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                throw new Exception("Invalid username or password");



            ///
            /// 		
            var isSubscriber = false;
            DateTime? expDate = null;
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            //if (userRole == UserRoles.Student)
            //{

            //    var subscription = await _unitOfWork.SubscriptionRepository.GetByCustomerIdAsync(user.CustomerId);

            //    if (subscription != null && subscription.Status == "active")
            //    {
            //        isSubscriber = true;
            //        expDate = subscription.CurrentPeriodEnd;
            //    }
            //    else
            //    {
            //        expDate = DateTime.Now.AddDays(7);
            //    }
            //}
            if (userRole == UserRoles.Instructor)
            {
                var instructor = await _instructorsService.IsInstructorVerified(user.Id);
                isInstructorVerified = (bool)instructor?.GetType().GetProperty("isVerified")?.GetValue(instructor, null);
            }
            
            return new UserDto
            {
                Email = user.Email,
                Username = user.UserName!,
                Token = await _tokenService.CreateToken(user),
                IsInstructorVerified = isInstructorVerified
            };
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            if (await UserEmailExists(registerDto.Email))
                throw new Exception("This Email is already registered");
            if (await UserUsernameExists(registerDto.Username))
                throw new Exception("Username is taken");


            if (registerDto.Role != UserRoles.Student && registerDto.Role != UserRoles.Instructor)
                throw new Exception("Not allowed");
                
            AppUser user = CreateUser(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault()?.Description);

            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role);

            if (!roleResult.Succeeded)
                throw new Exception(roleResult.Errors.FirstOrDefault()?.Description);

            var userDto = new UserDto
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = await _tokenService.CreateToken(user),
                IsInstructorVerified = false
            };

            return userDto;
        }


        private AppUser CreateUser(RegisterDto registerDto)
        {
            if (string.Compare(UserRoles.Student, registerDto.Role) == 0)
            {
                return new Student
                {
                    UserName = registerDto.Username.ToLower(),
                    Email = registerDto.Email,
                    Name = registerDto.Name
                };
            }
            else if (string.Compare(UserRoles.Instructor, registerDto.Role) == 0)
            {
                return new Instructor
                {
                    UserName = registerDto.Username.ToLower(),
                    Email = registerDto.Email,
                    Name = registerDto.Name,
                    Biography = registerDto.Biography
                };
            }

            throw new ArgumentException("Invalid role");
        }

        private async Task<bool> UserEmailExists(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        private async Task<bool> UserUsernameExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper());
        }
    }
}
