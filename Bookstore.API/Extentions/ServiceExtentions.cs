using Bookstore.Repositories;
using Bookstore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Bookstore.API.Extentions
{
    public static class ServiceExtentions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
     services.AddDbContext<AppDbContext>(opts =>
         opts.UseSqlite(configuration.GetConnectionString("sqlConnection")));


        public static void ConfigureBookService(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IDemoRepository, DemoRepository>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IDemoService, DemoService>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationService>();
        }
    }
}