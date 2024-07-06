using Microsoft.AspNetCore.Identity;


namespace e_learning.Domain.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public List<AppUserRole> UserRoles { get; set; }
    }
}
