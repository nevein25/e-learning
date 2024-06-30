using API.Entities;
using System.Linq.Expressions;

namespace API.Repositories.Interfaces
{
    public interface ILessonRepositry
    {
        public Task<IEnumerable<Lesson>> Find(Expression<Func<Lesson, bool>> predicate);
        public Lesson MapToLesson<T>(T lessonDto) where T : class;

        public Task<bool> Add(Lesson lesson);
        public Task<int> GetLessonCountByCourseId(int courseId);
    }
}
