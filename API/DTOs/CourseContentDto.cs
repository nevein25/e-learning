namespace API.DTOs
{
    public class CourseContentDto
    {
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

        public string image { get; set; }
        public List<ModuleContentDTO> Modules { get; set; }
    }
}
