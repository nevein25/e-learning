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

        public static void SeedCoursesWithDependencies(EcommerceContext context)
        {
            if (!context.Instructors.Any())
            {
                var instructors = new List<Instructor>
        {
            new Instructor { Name = "John Doe", Biography = "Expert in programming." },
            new Instructor { Name = "Jane Smith", Biography = "Database specialist." },
            new Instructor { Name = "Alice Johnson", Biography = "Web development guru." },
            new Instructor { Name = "Robert Brown", Biography = "Machine learning enthusiast." },
            new Instructor { Name = "Emily Davis", Biography = "Cybersecurity expert." },
            new Instructor { Name = "Michael Wilson", Biography = "Network engineering professional." },
            new Instructor { Name = "Emma Clark", Biography = "Mobile app development specialist." },
            new Instructor { Name = "James Lewis", Biography = "Cloud computing advocate." },
            new Instructor { Name = "Olivia Walker", Biography = "Data science expert." },
            new Instructor { Name = "David Hall", Biography = "Software architecture wizard." }
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
            new Category { Name = "Web Development" },
            new Category { Name = "Machine Learning" },
            new Category { Name = "Cybersecurity" },
            new Category { Name = "Network Engineering" },
            new Category { Name = "Mobile App Development" },
            new Category { Name = "Cloud Computing" },
            new Category { Name = "Data Science" },
            new Category { Name = "Software Architecture" }
        };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Courses.Any())
            {
                var instructors = context.Instructors.Take(10).ToList();
                var categories = context.Categories.Take(10).ToList();

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
                InstructorId = instructors[0].Id,
                CategoryId = categories[0].Id
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
                InstructorId = instructors[1].Id,
                CategoryId = categories[1].Id
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
                InstructorId = instructors[2].Id,
                CategoryId = categories[2].Id
            },
            new Course
            {
                Name = "Machine Learning Basics",
                Duration = 12.0m,
                Description = "Introduction to machine learning concepts",
                Price = 120.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "machine_learning_basics.jpg",
                InstructorId = instructors[3].Id,
                CategoryId = categories[3].Id
            },
            new Course
            {
                Name = "Cybersecurity Fundamentals",
                Duration = 18.0m,
                Description = "Learn the fundamentals of cybersecurity",
                Price = 180.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "cybersecurity_fundamentals.jpg",
                InstructorId = instructors[4].Id,
                CategoryId = categories[4].Id
            },
            new Course
            {
                Name = "Network Engineering 101",
                Duration = 14.0m,
                Description = "Basics of network engineering",
                Price = 140.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "network_engineering_101.jpg",
                InstructorId = instructors[5].Id,
                CategoryId = categories[5].Id
            },
            new Course
            {
                Name = "Mobile App Development for Beginners",
                Duration = 16.0m,
                Description = "Learn how to develop mobile apps",
                Price = 160.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "mobile_app_development.jpg",
                InstructorId = instructors[6].Id,
                CategoryId = categories[6].Id
            },
            new Course
            {
                Name = "Introduction to Cloud Computing",
                Duration = 13.0m,
                Description = "Basics of cloud computing",
                Price = 130.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "intro_to_cloud_computing.jpg",
                InstructorId = instructors[7].Id,
                CategoryId = categories[7].Id
            },
            new Course
            {
                Name = "Data Science for Everyone",
                Duration = 17.0m,
                Description = "Introduction to data science concepts",
                Price = 170.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "data_science_for_everyone.jpg",
                InstructorId = instructors[8].Id,
                CategoryId = categories[8].Id
            },
            new Course
            {
                Name = "Software Architecture Principles",
                Duration = 19.0m,
                Description = "Learn the principles of software architecture",
                Price = 190.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "software_architecture_principles.jpg",
                InstructorId = instructors[9].Id,
                CategoryId = categories[9].Id
            },
            new Course
            {
                Name = "Object-Oriented Programming",
                Duration = 11.0m,
                Description = "Learn the basics of OOP",
                Price = 110.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "oop_basics.jpg",
                InstructorId = instructors[0].Id,
                CategoryId = categories[0].Id
            },
            new Course
            {
                Name = "SQL for Beginners",
                Duration = 13.5m,
                Description = "Introduction to SQL",
                Price = 135.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "sql_for_beginners.jpg",
                InstructorId = instructors[1].Id,
                CategoryId = categories[1].Id
            },
            new Course
            {
                Name = "JavaScript Essentials",
                Duration = 14.5m,
                Description = "Learn the essentials of JavaScript",
                Price = 145.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "javascript_essentials.jpg",
                InstructorId = instructors[2].Id,
                CategoryId = categories[2].Id
            },
            new Course
            {
                Name = "Deep Learning with Python",
                Duration = 21.0m,
                Description = "Learn deep learning with Python",
                Price = 210.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "deep_learning_python.jpg",
                InstructorId = instructors[3].Id,
                CategoryId = categories[3].Id
            },
            new Course
            {
                Name = "Network Security Basics",
                Duration = 12.5m,
                Description = "Learn the basics of network security",
                Price = 125.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "network_security_basics.jpg",
                InstructorId = instructors[4].Id,
                CategoryId = categories[4].Id
            },
            new Course
            {
                Name = "Linux Administration",
                Duration = 20.0m,
                Description = "Learn the essentials of Linux administration",
                Price = 200.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "linux_administration.jpg",
                InstructorId = instructors[5].Id,
                CategoryId = categories[5].Id
            },
            new Course
            {
                Name = "iOS App Development",
                Duration = 18.5m,
                Description = "Learn to develop iOS apps",
                Price = 185.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "ios_app_development.jpg",
                InstructorId = instructors[6].Id,
                CategoryId = categories[6].Id
            },
            new Course
            {
                Name = "AWS Cloud Practitioner",
                Duration = 16.0m,
                Description = "Introduction to AWS Cloud",
                Price = 160.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "aws_cloud_practitioner.jpg",
                InstructorId = instructors[7].Id,
                CategoryId = categories[7].Id
            },
            new Course
            {
                Name = "Big Data Analytics",
                Duration = 22.0m,
                Description = "Learn the basics of big data analytics",
                Price = 220.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "big_data_analytics.jpg",
                InstructorId = instructors[8].Id,
                CategoryId = categories[8].Id
            },
            new Course
            {
                Name = "Microservices Architecture",
                Duration = 19.5m,
                Description = "Learn the principles of microservices architecture",
                Price = 195.00m,
                Language = "English",
                UploadDate = DateTime.Now,
                Thumbnail = "microservices_architecture.jpg",
                InstructorId = instructors[9].Id,
                CategoryId = categories[9].Id
            }
        };

                context.Courses.AddRange(courses);
                context.SaveChanges();
            }
        }

    }
}
