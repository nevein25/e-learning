using API.Context;
using API.Repositories.Classes;
using API.Repositories.Interfaces;
using API.Services.Classes;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
               IConfiguration config)
        {
            services.AddControllers();
            services.AddDbContext<EcommerceContext>(op =>
              op.UseSqlServer(config.GetConnectionString("MyConnection"))
            );

            services.AddCors();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IPhotoService, PhotoService>();



            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
