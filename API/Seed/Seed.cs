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
                Description = "Programming introduces learners to the fundamental concepts, principles, and techniques used to write, test, and debug computer programs. It covers essential topics such as variables, data types, control structures (like loops and conditionals), functions, and basic algorithms. Students often start with a high-level programming language like Python or Java, which offer simplicity and versatility for learning key programming concepts. ",
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
                Description = "Advanced databases explore sophisticated topics in database design, optimization, and management. They cover advanced data modeling techniques, such as normalization beyond third normal form, and discuss methods for handling large-scale databases, including distributed databases and sharding techniques. ",
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
                Description = "Web development with .NET leverages the robust features of the .NET framework, including ASP.NET (Active Server Pages), ASP.NET Core, and other tools like C#, Visual Basic, and Entity Framework. Developers use these technologies to build scalable, secure, and high-performance web applications that can run on Windows servers or cross-platform environments.",
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
                Description = "Machine learning basics encompass algorithms and statistical models that enable computers to improve their performance on tasks through experience (data). Key concepts include supervised learning, unsupervised learning, and reinforcement learning.",
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
                Description = "Cybersecurity fundamentals encompass the foundational principles and practices designed to protect computer systems, networks, and data from unauthorized access, attacks, and damage. ",
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
                Description = "Network Engineering 101 equips participants with foundational knowledge and practical skills necessary for pursuing careers in network administration, telecommunications, and IT infrastructure management. It serves as a solid starting point for building expertise in designing and maintaining robust, scalable networks that support modern business operations and technological advancements.",
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
                var Courses = context.Courses.Take(10).ToList();
                var Modules = new List<Module>
                {
                    new Module
                    {
                        Name="Variables and Data Types",
                        CourseId=Courses[0].Id,
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Operators",
                        CourseId=Courses[0].Id,
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Query Optimization",
                        CourseId=Courses[1].Id,
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Performance Tuning",
                        CourseId=Courses[1].Id,
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Web APIs",
                        CourseId=Courses[2].Id,
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Razor Pages",
                        CourseId=Courses[2].Id,
                        ModuleNumber=2
                    },

                    new Module
                    {
                        Name="Supervised Learning",
                        CourseId=Courses[3].Id,
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="UnSupervised Learning",
                        CourseId=Courses[3].Id,
                        ModuleNumber=2
                    },
                    new Module
                    {
                        Name="Cryptography",
                        CourseId=Courses[4].Id,
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Introduction to Cybersecurity",
                        CourseId=Courses[4].Id,
                        ModuleNumber=2
                    },
                    new Module
                    {
                        Name="IP Addressing",
                        CourseId=Courses[5].Id,
                        ModuleNumber=1
                    },
                    new Module
                    {
                        Name="Routing and Switching",
                        CourseId=Courses[5].Id,
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
                        Content="Primitive data types in programming languages represent the fundamental building blocks for storing and manipulating data. These data types are typically supported directly by the language and are used to define variables, constants, and function parameters.",
                        ModuleId=Modules[0].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Arithmetic Operators",
                        Type=LessonType.Video,
                        Content="Arithmetic operators in programming languages are fundamental tools for performing mathematical calculations and operations on numerical data types. These operators include addition (+), subtraction (-), multiplication (*), division (/), and modulus (%).",
                        ModuleId=Modules[1].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Query Execution",
                        Type=LessonType.Video,
                        Content="Query execution in the context of databases refers to the process by which a database management system (DBMS) processes and executes a query written in a query language like SQL (Structured Query Language).",
                        ModuleId=Modules[2].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Query Optimization",
                        Type=LessonType.Video,
                        Content="Query optimization in databases is the process of improving the performance of queries to minimize execution time and resource usage while maximizing efficiency. This optimization is crucial for enhancing database performance, especially in systems handling large volumes of data or complex queries",
                        ModuleId=Modules[3].Id,
                        LessonNumber=1
                    },
                       new Lesson
                    {
                        Name="RESTful APIs",
                        Type=LessonType.Video,
                        Content="RESTful APIs (Representational State Transfer APIs) are a widely used architectural style for designing networked applications and services. They adhere to a set of principles that promote scalability, simplicity, and interoperability between systems",
                        ModuleId=Modules[4].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Routing",
                        Type=LessonType.Video,
                        Content="Routing in the context of networking and web development refers to the process of determining the path that data packets or requests take from their source to their destination across a network or within a web application.",
                        ModuleId=Modules[5].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Data Preprocessing",
                        Type=LessonType.Video,
                        Content="Data preprocessing is a critical step in data mining and machine learning pipelines, involving the transformation and preparation of raw data before it is fed into a model for analysis. ",
                        ModuleId=Modules[6].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Regression",
                        Type=LessonType.Video,
                        Content="Regression analysis is a statistical method used to examine the relationship between one dependent variable (often denoted as \r\n𝑌\r\nY)",
                        ModuleId=Modules[7].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Digital Signatures",
                        Type=LessonType.Video,
                        Content="Digital signatures are cryptographic mechanisms used to verify the authenticity, integrity, and non-repudiation of digital messages or documents. ",
                        ModuleId=Modules[8].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Cybersecurity Threats",
                        Type=LessonType.Video,
                        Content="Cybersecurity threats encompass a broad range of malicious activities and risks targeting digital information systems, networks, and data. These threats pose significant challenges to individuals, organizations, and governments worldwide.",
                        ModuleId=Modules[9].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Bus Topology",
                        Type=LessonType.Video,
                        Content="Bus topology is a type of network architecture in which all devices (nodes) are connected to a single central cable, known as the bus or backbone",
                        ModuleId=Modules[10].Id,
                        LessonNumber=1
                    },
                    new Lesson
                    {
                        Name="Router",
                        Type=LessonType.Video,
                        Content="A router is a networking device responsible for forwarding data packets between computer networks. It operates at the network layer (Layer 3) of the OSI model and plays a crucial role in directing traffic efficiently across interconnected networks. ",
                        ModuleId=Modules[11].Id,
                        LessonNumber=1
                    },
                  

                };
                context.Lessons.AddRange(Lessons);
                context.SaveChanges();
            }

        }
    }
}
