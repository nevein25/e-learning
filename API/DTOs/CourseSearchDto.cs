namespace API.DTOs
{
    public class CourseSearchDto
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 9; // Default to 10 items per page
    }
}
