using API.Infrastructure.Context;
using CloudinaryDotNet;
using e_learning.Application.Services.Interfaces;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;
using e_learning.Domain.Services;
using e_learning.Infrastructure.Configurations;
using e_learning.Infrastructure.Repositories;
using e_learning.Infrastructure.Seeders;
using e_learning.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace e_learning.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ElearningContext>(options =>
                                    options.UseSqlServer(configuration.GetConnectionString("MyConnection")));


            services
               .AddIdentityCore<AppUser>(opt =>
               {
                   // here you can add options for pass like (length, should have numbers), mail and others
                   opt.Password.RequireNonAlphanumeric = false;
               })
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddSignInManager<SignInManager<AppUser>>()
               .AddEntityFrameworkStores<ElearningContext>();

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenKey = configuration["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
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


            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.AddScoped<Cloudinary>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                return new Cloudinary(new CloudinaryDotNet.Account(
                    settings.CloudName,
                    settings.ApiKey,
                    settings.ApiSecret
                ));
            });

            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:PrivateKey");
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            services.AddScoped<IFileService, Application.Services.FileService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVideoService, VideoOnCloudinary>();
            services.AddScoped<IStripeService, StripeService>();

            services.AddScoped<ISeed, Seed>();

            services.AddScoped<IRatesRepository, RateRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IWishlistsRepository, WishlistsRepository>();
            services.AddScoped<ICoursesPurshasedRepository, CoursesPurshasedRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IInstructorsRepository, InstructorsRepository>();
            services.AddScoped<IModulesRepositry, ModulesRepositry>();
            services.AddScoped<ILessonsRepository, LessonsRepository>();



        }
    }
}
