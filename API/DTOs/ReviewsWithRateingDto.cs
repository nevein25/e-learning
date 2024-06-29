using API.Entities;

namespace API.DTOs
{
    public class ReviewsWithRateingDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string Username { get; set; }
        public int StudentId { get; set; }

        public string Picture { get; set; }
        public int Stars { get; set; }



    }
}
