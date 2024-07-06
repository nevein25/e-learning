using e_learning.Application.Courses.DTOs;
using e_learning.Application.Lessons.DTOs;
using e_learning.Domain.Common;

namespace e_learning.Application.Lessons
{
    public interface ILessonsService
    {
        Task<int> GetLessonCountByCourseId(int id);
        Task<MediaUploadResult?> CreateLessonAsync(LessonDto lessonDto);
        Task<CourseContentDto> GetCourseWithLessonsContent(int courseId);
    }
}
