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
    }

}
