namespace API.Entities
{
    public class Instructor : AppUser
    {
        public string Biography { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
