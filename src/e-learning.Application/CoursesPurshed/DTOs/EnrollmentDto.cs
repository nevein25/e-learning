namespace e_learning.Application.CoursesPurshed.DTOs
{
    public class EnrollmentDto
    {
        public int StudentId { get; set; }
        public string CourseId { get; set; }
        public bool IsFinished { get; set; } = false;
        public double Progress { get; set; }
        public List<int> VisitedLessons { get; set; }
        public DateOnly FinishedDate { get; set; }
    }
}
