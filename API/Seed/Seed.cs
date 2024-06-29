using API.Common;
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

        public static async Task SeedCoursesWithDependenciesAsync(EcommerceContext context,
          UserManager<AppUser> userManager)
        {
            if (!context.Instructors.Any())
            {
                var instructors = new List<Instructor>
                {
                    new Instructor {UserName="JohnDoe",  Name = "John Doe", Picture="team-1.jpg", Biography = "Expert in programming." },
                    new Instructor {UserName="JaneSmith", Name = "Jane Smith", Picture="team-2.jpg", Biography = "Database specialist." },
                    new Instructor {UserName="AliceJohnson", Name = "Alice Johnson", Picture="team-4.jpg", Biography = "Web development guru." },
                    new Instructor {UserName="RobertBrown", Name = "Robert Brown", Picture="team-3.jpg", Biography = "Machine learning enthusiast." },
                    new Instructor {UserName="EmilyDavis", Name = "Emily Davis", Biography = "Cybersecurity expert." },
                    new Instructor {UserName="MichaelWilson", Name = "Michael Wilson", Biography = "Network engineering professional." },
                    new Instructor {UserName="EmmaClark", Name = "Emma Clark", Biography = "Mobile app development specialist." },
                    new Instructor {UserName="JamesLewis", Name = "James Lewis", Biography = "Cloud computing advocate." },
                    new Instructor {UserName="OliviaWalker", Name = "Olivia Walker", Biography = "Data science expert." },
                    new Instructor {UserName="DavidHall", Name = "David Hall", Biography = "Software architecture wizard." }
                };

                foreach (var instructor in instructors)
                {
                    await userManager.CreateAsync(instructor, "TEST@test123");
                    await userManager.AddToRoleAsync(instructor, "Instructor");

                }

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

            Seed.buildModules(context);
            Seed.buildLessons(context);
        }


        private static void buildModules(EcommerceContext context)
        {
            if (!context.Modules.Any())
            {
                var courses = context.Courses.Take(10).ToList();

                var Modules = new List<Module>
                {
                    new Module
                    {
                        Name="Variables and Data Types",
                        CourseId=1,
                        Course=courses[0],
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Operators",
                        Course=courses[0],
                        CourseId=1,
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Query Optimization",
                        CourseId=2,
                        Course=courses[1],
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Performance Tuning",
                        CourseId=2,
                        Course=courses[1],
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Web APIs",
                        CourseId=3,
                        Course=courses[2],
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Razor Pages",
                        CourseId=3,
                        Course=courses[2],
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Supervised Learning",
                        CourseId=4,
                        Course=courses[3],
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="UnSupervised Learning",
                        CourseId=4,
                        Course=courses[3],
                        ModuleNumber=2
                    },
                    new Module
                    {
                        Name="Cryptography",
                        CourseId=5,
                        Course=courses[4],
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Introduction to Cybersecurity",
                        CourseId=5,
                        Course=courses[4],
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Network Topologies",
                        CourseId=6,
                        Course=courses[5],
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Networking Devices",
                        CourseId=6,
                        Course=courses[5],
                        ModuleNumber=2
                    }
                };
                context.Modules.AddRange(Modules);
                context.SaveChanges();
            }
        }

        private static void buildLessons(EcommerceContext context)
        {
            if (!context.Lessons.Any())
            {
                var Modules = context.Modules.Take(12).ToList();
                var Lessons = new List<Lesson>
                {
                    new Lesson
                    {
                        Name="Primitive Data Types",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-1",
                        Module=Modules[0],
                        ModuleId=1,
                        LessonNumber=1
                    },

                    new Lesson
                    {
                        Name="Arithmetic Operators",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-2",
                        Module=Modules[1],
                        ModuleId=2,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Execution Plans",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-3",
                        Module=Modules[2],
                        ModuleId=3,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Monitoring",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-4",
                        Module=Modules[3],
                        ModuleId=4,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="HTTP Methods",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-5",
                        Module=Modules[4],
                        ModuleId=5,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Page Models",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-6",
                        Module=Modules[5],
                        ModuleId=6,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Evaluation Metrics",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-7",
                        Module=Modules[6],
                        ModuleId=7,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Clustering Algorithms",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-8",
                        Module=Modules[7],
                        ModuleId=8,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Hashing",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-9",
                        Module=Modules[8],
                        ModuleId=9,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Endpoint Security",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-10",
                        Module=Modules[9],
                        ModuleId=10,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Ring Topology",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-11",
                        Module=Modules[10],
                        ModuleId=11,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Router",
                        Type=LessonType.Video,
                        IsDeleted=false,
                        Content="Lesson Content-12",
                        Module=Modules[11],
                        ModuleId=12,
                        LessonNumber=1
                    }


                };
                context.Lessons.AddRange(Lessons);
                context.SaveChanges();
            }
        }

    }
}
