using Microsoft.IdentityModel.Tokens;

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
        public string VisitedLessons { get; set; }
        public List<int> GetVisitedLessons()
        {
            return string.IsNullOrEmpty(VisitedLessons)
                ? new List<int>()
                : VisitedLessons.Split(',').Select(int.Parse).ToList();
        }

        public void SetVisitedLessons(List<int> lessons)
        {
            VisitedLessons = string.Join(",", lessons);
        }
    }
}