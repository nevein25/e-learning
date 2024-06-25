namespace API.DTOs
{
    public class ModuleContentDTO
    {
        public string Name { get; set; }
        public int ModuleNumber { get; set; }

        public List<LessonContentDto>Lessons{get;set;}
    }
}
