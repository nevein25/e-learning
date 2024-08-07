﻿namespace e_learning.Application.DTOs.Auth
{
    public class UserDto
    {
        public required string Email { get; set; }
        public required string Username { get; set; }

        public required string Token { get; set; }
        public required bool IsInstructorVerified { get; set; } = false;

    }
}
