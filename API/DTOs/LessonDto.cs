using API.Common;

namespace API.DTOs
{
    public class LessonDto
    {
        public string Name { get; set; }
        public LessonType Type { get; set; } =  LessonType.Video;
        public string Content { get; set; }
      
        public int ModuleId { get; set; }
        public IFormFile? VideoContent { get; set; }
    }
}
