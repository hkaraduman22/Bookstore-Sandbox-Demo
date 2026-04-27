using Bookstore.API.Extentions;
using Bookstore.Repositories;
using Bookstore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Servisler
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureBookService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureAuthentication();

// JWT Doðrulama (Anahtarý Eþitledik)
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

// Middleware Sýralamasý
app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Veritabaný Baþlatma (Her seferinde silme iþlemini kaldýrdýk)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();