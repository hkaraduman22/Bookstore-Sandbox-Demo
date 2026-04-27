using Bookstore.API.Extentions;
using Bookstore.Presentation;
using Bookstore.Repositories;
using Bookstore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.AddControllers().AddApplicationPart(typeof(Bookstore.Presentation.AssemblyReference).Assembly);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173", "https://localhost:5173") // React'in varsayýlan portlarý
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

 
builder.Services.AddScoped<IDemoRepository, DemoRepository>();
builder.Services.AddScoped<IDemoService, DemoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();