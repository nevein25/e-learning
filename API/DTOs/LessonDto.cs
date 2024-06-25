using API.Common;

namespace API.DTOs
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LessonType Type { get; set; } // Video, Text
        public string Content { get; set; }
       // public int LessonNumber { get; set; }
        public int ModuleId { get; set; }
        public IFormFile? VideoContent { get; set; }
    }
}
