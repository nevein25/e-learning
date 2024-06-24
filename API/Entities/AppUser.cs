using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public required string Name { get; set; }
        public string? Picture { get; set; }
        public bool IsDeleted { get; set; } = false;

        // i think im gonna delete it
        public string? CustomerId { get; set; } // for stripe
        public List<AppUserRole> UserRoles { get; set; } = [];

    }
}
