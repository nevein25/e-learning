using e_learning.Application.Rates.DTOs;

namespace e_learning.Application.Rates
{
    public interface IRateService
    {
        Task<RateByUserDto> GetRateForLogedinStudent(int courseId, int studentId);
        Task RateAsync(int CourseId, int UserId, int NumOfStars);
        Task<int> GetAvgRateForCourseAsync(int courseId);
    }
}
