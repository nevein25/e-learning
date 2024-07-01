using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace API.Context
{
    public class EcommerceContext : IdentityDbContext<
            AppUser,
            AppRole,
            int,
            IdentityUserClaim<int>,
            AppUserRole,
            IdentityUserLogin<int>,
            IdentityRoleClaim<int>,
            IdentityUserToken<int>
            >
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        //public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Rate> Rates { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<CoursePurchase> CoursePurchases { get; set; }


        //public DbSet<PaymentDetail> PaymentDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /// Changing the default name for tables
            builder.Entity<AppUser>(entity => { entity.ToTable(name: "Users"); });
            builder.Entity<AppRole>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<AppUserRole>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<int>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<int>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityUserToken<int>>(entity => { entity.ToTable("UserTokens"); });
            builder.Entity<IdentityRoleClaim<int>>(entity => { entity.ToTable("RoleClaims"); });
            ///

            /// TPT approach
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<Admin>().ToTable("Admins");
            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Instructor>().ToTable("Instructors");
            ///


            /// [AppUser] * <have> * [AppRole]
            builder.Entity<AppUserRole>()
              .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder
                .Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            ///Student and Lesson
            builder.Entity<StudentLesson>()
           .HasKey(sl => new { sl.StudentId, sl.LessonId });

            builder.Entity<StudentLesson>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.StudentLessons)
                .HasForeignKey(sl => sl.StudentId);

            builder.Entity<StudentLesson>()
                .HasOne(sl => sl.Lesson)
                .WithMany(l => l.StudentLessons)
                .HasForeignKey(sl => sl.LessonId);
            ///

            ///Student and Course Enrollments
          //  builder.Entity<Enrollment>()
          //.HasKey(e => new { e.StudentId, e.CourseId });

          //  builder.Entity<Enrollment>()
          //      .HasOne(e => e.Student)
          //      .WithMany(s => s.Enrollments)
          //      .HasForeignKey(e => e.StudentId)
          //      .OnDelete(DeleteBehavior.Restrict);

          //  builder.Entity<Enrollment>()
          //      .HasOne(e => e.Course)
          //      .WithMany(c => c.Enrollments)
          //      .HasForeignKey(e => e.CourseId)
          //      .OnDelete(DeleteBehavior.Restrict);
            ///

            ///Student and Course Rates
            builder.Entity<Rate>()
          .HasKey(e => new { e.StudentId, e.CourseId });

            builder.Entity<Rate>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Rates)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rate>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Rates)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            ///

            ///Student and Course Wishlist
            builder.Entity<Wishlist>()
          .HasKey(e => new { e.StudentId, e.CourseId });

            builder.Entity<Wishlist>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Wishlists)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Wishlist>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Wishlists)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            ///

            // to solve decimal warning
            builder.Entity<Course>().Property(c=> c.Duration).HasColumnType("decimal(18,4)");
            builder.Entity<Course>().Property(c => c.Price).HasColumnType("decimal(18,4)");


        }


    }
}
