namespace e_learning.Domain.Entities
{
    public class Student : AppUser
    {
        public DateOnly? DOB { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Rate> Rates { get; set; }

    }
}
