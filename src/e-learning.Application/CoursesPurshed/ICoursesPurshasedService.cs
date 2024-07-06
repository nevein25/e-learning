using e_learning.Application.CoursesPurshed.DTOs;
using e_learning.Domain.Entities;

namespace e_learning.Application.CoursesPurshed
{
    public interface ICoursesPurshasedService
    {
        Task<IList<CoursesPurshasedDto>> CoursesBoughtByStudentId(int studentId);
        Task<bool> IsCourseBoughtAsync(int courseId, int studentId);
        Task<bool> IsStudentFinishedCourseAsync(int studentId, int courseId);
        Task<IList<CourseUploadedDto>> CoursesUploadedByInstructor(int instructorId);
        Task<CoursePurchase> GetEnrollmentAsync(int courseId, int UserId);
        Task<List<int>> GetVisitedLessonsByCourseId(string courseId);
        Task<EnrollmentOperationResult> AddOrUpdateEnrollment(EnrollmentDto enrollmentDto, int userId);
    }
}
