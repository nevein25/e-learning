using e_learning.Application.Auth;
using e_learning.Application.Categories;
using e_learning.Application.Certificates;
using e_learning.Application.Courses;
using e_learning.Application.CoursesPurshed;
using e_learning.Application.Instructors;
using e_learning.Application.Lessons;
using e_learning.Application.Modules;
using e_learning.Application.Rates;
using e_learning.Application.Reviews;
using e_learning.Application.WishLists;
using e_learning.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace e_learning.Application.Extensions

{
    public static class ServiceCollectionExtensions
    {

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IWishlistsService, WishlistsService>();
            services.AddScoped<ICoursesPurshasedService, CoursesPurshasedService>();
            services.AddScoped<ICoursesService, CoursesService>();
            services.AddScoped<IInstructorsService, InstructorsService>();
            services.AddScoped<ILessonsService, LessonsService>();
            services.AddScoped<IModulesService, ModulesService>();
            services.AddScoped<ICertifcatesService, CertifcatesService>();


        }
    }
}
