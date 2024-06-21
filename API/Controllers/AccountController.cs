﻿using API.Common;
using API.Context;
using API.DTOs;
using API.Entities;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System;
using API.Services.Interfaces;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserEmailExists(registerDto.Email))
                return BadRequest("This Email is already registered");

            if (await UserUsernameExists(registerDto.Username))
                return BadRequest("Username is taken");


            if (registerDto.Role != Roles.Student.ToString() && registerDto.Role != Roles.Instructor.ToString())
                return BadRequest("Not allowed");

            AppUser user = CreateUser(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role);

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            var userDto = new UserDto
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = await _tokenService.CreateToken(user)
            };

            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.Users.SingleOrDefaultAsync(
                x => x.Email == loginDto.EmailOrUsername || x.UserName == loginDto.EmailOrUsername
            );


            if (user == null) return Unauthorized("Invalid username or password");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                return Unauthorized("Invalid username or password");

            return new UserDto
            {
                Email = user.Email,
                Username = user.UserName!,
                Token = await _tokenService.CreateToken(user)
            };
        }
        private AppUser CreateUser(RegisterDto registerDto)
        {
            if (string.Compare(Roles.Student.ToString(), registerDto.Role) == 0)
            {
                return new Student
                {
                    UserName = registerDto.Username.ToLower(),
                    Email = registerDto.Email,
                    Name = registerDto.Name
                };
            }
            else if (string.Compare(Roles.Instructor.ToString(), registerDto.Role) == 0)
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
