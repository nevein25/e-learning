namespace API.Entities
{

    public class CoursePurchase
    {
        public int Id { get; set; }
        public string CourseId { get; set; }// should be int
        public int UserId { get; set; }

        public string CustomerId { get; set; } // i did not need it after all, but ok

        public bool IsFinished { get; set; } = false; // better to be calc but why not
        public double? Progress { get; set; }  // better to be calc but why not
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
