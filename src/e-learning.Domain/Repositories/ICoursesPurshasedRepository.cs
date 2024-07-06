using e_learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_learning.Domain.Repositories
{
    public interface ICoursesPurshasedRepository
    {
        Task<IList<CoursePurchase>> GetCoursePurchasesByStudentIdAsync(int studentId);
        Task<IList<Course>> GetCoursesByIdsAsync(List<int> courseIds);
        Task<bool> IsCourseBoughtExisitAsync(int courseId, int studentId);
        Task<CoursePurchase> GetCoursePurchaseAsync(int studentId, int courseId);
        Task<IList<Course>> GetCoursesByInstructorIdAsync(int instructorId);
        Task<IList<CoursePurchase>> GetCoursePurchasesByCourseIdAsync(string courseId);
        Task CreateCoursePurchaseAsync(CoursePurchase coursePurchase);
        Task<bool> UpdateCoursePurchaseAsync(CoursePurchase enrollment);

        Task<bool> AddCoursePurchaseAsync(CoursePurchase enrollment);

        Task<CoursePurchase> GetCoursePurchasesByCourseId(string courseId);

    }
}
