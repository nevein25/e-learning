using API.Context;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    // here you can add options for pass like (length, should have numbers), mail and others
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddEntityFrameworkStores<EcommerceContext>();

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(tokenKey)
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });

            services.AddAuthorization(opt =>
            { // there are many polices you can choose, like you can say   policy => policy.RequireAuthenticatedUser
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Student"));
                opt.AddPolicy("RequireInstructorRole", policy => policy.RequireRole("Instructor"));
                opt.AddPolicy(
                    "RequireAdminAndInstructorRole",
                    policy => policy.RequireRole("Admin", "Instructor")
                );
            });
            return services;
        }
    }
}
