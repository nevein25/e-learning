using e_learning.Domain.Common;

namespace e_learning.Application.Lessons.DTOs
{
    public class LessonContentDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public LessonType Type { get; set; } = LessonType.Video;
        public string Content { get; set; }
        public int LessonNumber { get; set; }
    }
}
