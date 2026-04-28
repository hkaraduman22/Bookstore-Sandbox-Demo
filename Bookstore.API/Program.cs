using Bookstore.API.Extentions;
using Bookstore.Repositories;
using Bookstore.Services;
using Bookstore.Services.Conctrats;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// SERVÝCES REGISTRATION
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureBookService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureLoggingService();

// JWT Validation
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BookstoreAPI",
            ValidAudience = "BookstoreFrontend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HakanTarik_Cok_Gizli_Ve_Cok_Uzun_Anahtar_2026_!_"))
        };
    });

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Bookstore.Presentation.AssemblyReference).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggingService>();


// Middleware Sýralamasý
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
 
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
     
    dbContext.Database.EnsureCreated();
     
    if (!dbContext.Categories.Any())
    {
        var demoService = scope.ServiceProvider.GetRequiredService<IDemoService>();
         
        demoService.ResetSystemToDemoAsync().GetAwaiter().GetResult();

        Console.WriteLine("Veritabaný boþtu, baþlangýç kategorileri ve kitaplarý eklendi.");
    }
}

app.Run();