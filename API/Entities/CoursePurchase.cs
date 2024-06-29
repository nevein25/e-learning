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
    }

}
