using API.Common;

namespace API.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LessonType Type { get; set; } // Video, Text
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public int LessonNumber { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set; }

    }
}