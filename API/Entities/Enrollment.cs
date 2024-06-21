namespace API.Entities
{
    public class Enrollment
    {
        public bool IsFinished { get; set; } // no need? it's calc
        public double Progress { get; set; } // no need? it's calc
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}