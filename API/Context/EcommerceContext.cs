using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

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

            ///
        }


        }
    }
