namespace API.DTOs
{
    public class ReviewAddDto
    {

        public int CourseId { get; set; }

        public string Text { get; set; }
    }

    public class ReviewUpdateDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }

        public string Text { get; set; }
    }

}
