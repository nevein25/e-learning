namespace API.DTOs
{
    public class CourseImgDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Language { get; set; }
        public IFormFile Thumbnail { get; set; }
         public string?img  { get; set; }
        public DateTime UploadDate { get; set; }
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }
    }
}
