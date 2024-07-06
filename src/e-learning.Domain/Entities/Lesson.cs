using e_learning.Domain.Common;

namespace e_learning.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LessonType Type { get; set; } = LessonType.Video;
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public int LessonNumber { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }


    }
}