using API.Context;
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

        public static void SeedCourses(EcommerceContext context)
        {
            if (!context.Instructors.Any())
            {
                var instructors = new List<Instructor>
                {
                    new Instructor { Name = "John Doe", Biography = "Expert in programming." },
                    new Instructor { Name = "Jane Smith", Biography = "Database specialist." }
                };
                context.Instructors.AddRange(instructors);
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Programming" },
                    new Category { Name = "Databases" },
                    new Category { Name = "Web Development" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Courses.Any())
            {
                var instructorJohn = context.Instructors.First(i => i.Name == "John Doe");
                var instructorJane = context.Instructors.First(i => i.Name == "Jane Smith");

                var programmingCategory = context.Categories.First(c => c.Name == "Programming");
                var databasesCategory = context.Categories.First(c => c.Name == "Databases");
                var webDevCategory = context.Categories.First(c => c.Name == "Web Development");

                var courses = new List<Course>
                {
                    new Course
                    {
                        Name = "Introduction to Programming",
                        Duration = 10.5m,
                        Description = "Learn the basics of programming",
                        Price = 99.99m,
                        Language = "English",
                        UploadDate = DateTime.Now,
                        Thumbnail = "intro_to_programming.jpg",
                        InstructorId = instructorJohn.Id,
                        CategoryId = programmingCategory.Id
                    },
                    new Course
                    {
                        Name = "Advanced Databases",
                        Duration = 15.0m,
                        Description = "Learn advanced database concepts",
                        Price = 199.99m,
                        Language = "English",
                        UploadDate = DateTime.Now,
                        Thumbnail = "advanced_databases.jpg",
                        InstructorId = instructorJane.Id,
                        CategoryId = databasesCategory.Id
                    },
                    new Course
                    {
                        Name = "Web Development with .NET",
                        Duration = 20.0m,
                        Description = "Learn web development with .NET",
                        Price = 149.99m,
                        Language = "English",
                        UploadDate = DateTime.Now,
                        Thumbnail = "web_dev_dotnet.jpg",
                        InstructorId = instructorJohn.Id,
                        CategoryId = webDevCategory.Id
                    }
                };

                context.Courses.AddRange(courses);
                context.SaveChanges();
            }
        }
    }
}
