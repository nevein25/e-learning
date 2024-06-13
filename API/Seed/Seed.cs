using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Seed
{
    public class Seed
    {
        public static async Task SeedRoles(
          RoleManager<AppRole> roleManager
      )
        {
            // if there are roles in the db exsist no need to seed data
            if (await roleManager.Roles.AnyAsync())
                return;


            var roles = new List<AppRole>
            {
                new AppRole { Name = "Admin" },
                new AppRole { Name = "Student" },
                new AppRole { Name = "Instructor" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

        }
    }
}