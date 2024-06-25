using API.Context;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using API.Repositories.Classes;
using API.Repositories.Interfaces;
using API.Seed;
using API.Services.Classes;
using API.Services.Interfaces;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.



StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeSettings:PrivateKey");
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));



builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cloudinary Services Configration
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<Cloudinary>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    return new Cloudinary(new CloudinaryDotNet.Account(
        settings.CloudName,
        settings.ApiKey,
        settings.ApiSecret
    ));
});
builder.Services.AddScoped<IVideoService, VideoOnCloudinary>();


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();




using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    var context = services.GetRequiredService<EcommerceContext>();

    // making the db from migrations if the db does not exsist
    await context.Database.MigrateAsync();

    await Seed.SeedRoles(roleManager);

    Seed.SeedCoursesWithDependencies(context);

}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
