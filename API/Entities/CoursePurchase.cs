namespace API.Entities
{

    public class CoursePurchase
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public int UserId { get; set; }

        public string CustomerId { get; set; } 

        public bool IsFinished { get; set; } = false; 
        public double? Progress { get; set; }  
        public DateTime PurchaseDate { get; set; }
        public DateOnly FinishedDate { get; set; }
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
