namespace API.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}