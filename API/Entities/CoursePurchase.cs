namespace API.Entities
{

    public class CoursePurchase
    {
        public int Id { get; set; }
        public string CourseId { get; set; }// should be int
        public int UserId { get; set; }

        public string CustomerId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

}
