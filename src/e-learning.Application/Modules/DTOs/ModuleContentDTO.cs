using e_learning.Application.Lessons.DTOs;

namespace e_learning.Application.Modules.DTOs

{
    public class ModuleContentDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int ModuleNumber { get; set; }

        public List<LessonContentDto> Lessons { get; set; }
    }
}
