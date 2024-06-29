namespace API.Entities
{
    public class Instructor : AppUser
    {
        public string? Biography { get; set; }
        public string? Paper { get; set; }
        public string? PaperPublicId { get; set; }
        public bool IsVerified { get; set; } = false;



        public ICollection<Course> Courses { get; set; }
    }
}
