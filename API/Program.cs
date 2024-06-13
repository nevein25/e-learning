using API.Context;
using API.Entities;
using API.Repositories.Classes;
using API.Repositories.Interfaces;
using API.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddDbContext<EcommerceContext>(op =>
      op.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
);


builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<EcommerceContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials()
                   .WithOrigins("http://localhost:4200");
        });
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();
