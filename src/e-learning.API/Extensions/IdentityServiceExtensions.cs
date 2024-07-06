namespace e_learning.API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

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
