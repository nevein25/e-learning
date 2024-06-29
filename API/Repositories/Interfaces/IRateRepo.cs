﻿using API.DTOs;
using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IRateRepo
    {
        public Task RateAsync(int CourseId, int UserId, int NumOfStars);
        public bool CourseExist(int courseId);
        Task<RateByUserDto> GetRateForStudentAsync(int courseId, int studentId);

    }
}
