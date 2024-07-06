using System.Reflection;


namespace e_learning.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Language { get; set; }
        public DateTime UploadDate { get; set; }
        public string Thumbnail { get; set; }

        public ICollection<Module>? Modules { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        //public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Rate>? Rates { get; set; }
        public ICollection<Wishlist>? Wishlists { get; set; }

        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

}
