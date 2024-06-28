using API.Common;

namespace API.DTOs
{
    public class LessonContentDto
    {
        public string Name { get; set; }
        public LessonType Type { get; set; } // Video, Text
        public string Content { get; set; }
        public int LessonNumber { get; set; }
    }
}
