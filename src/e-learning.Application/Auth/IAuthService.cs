using e_learning.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace e_learning.Application.Auth
{
    public interface IAuthService
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
    }
}
