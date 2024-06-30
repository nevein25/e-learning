using API.Entities;

namespace API.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<bool> AddEnrollmentAsync(CoursePurchase enrollment);
        Task<CoursePurchase> GetEnrollmentAsync(int userId, string courseId);
        Task<bool> UpdateEnrollmentAsync(CoursePurchase enrollment);
        public CoursePurchase MapToEnrollment<T>(T EnrollmentDto) where T : class;

        public Task<List<int>> GetVisitedLessonsByCourseId(string courseId);
    }
}
