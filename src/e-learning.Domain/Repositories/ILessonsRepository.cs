using e_learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_learning.Domain.Repositories
{
    public interface ILessonsRepository
    {
        Task<IEnumerable<Lesson>> FindLesson(Expression<Func<Lesson, bool>> predicate);


        Task<bool> AddLesson(Lesson lesson);

        Task<int> GetLessonCountByCourseId(int courseId);
    }
}
