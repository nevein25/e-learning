using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public required string Name { get; set; }
        public string? Picture { get; set; }
        public bool IsDeleted { get; set; } = false;


        public List<AppUserRole> UserRoles { get; set; } = [];

    }
}
