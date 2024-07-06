using Microsoft.AspNetCore.Http;

namespace e_learning.Application.Courses.DTOs
{
    public class CourseImgDto
    {
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Language { get; set; }
        public IFormFile Thumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
