namespace e_learning.Application.Courses.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Language { get; set; }
        //public IFormFile Thumbnail { get; set; }
         public string Thumbnail { get; set; }
        public DateTime UploadDate { get; set; }
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }
        public string Instructor { get; set; }
        public string Category { get; set; }
    }
}