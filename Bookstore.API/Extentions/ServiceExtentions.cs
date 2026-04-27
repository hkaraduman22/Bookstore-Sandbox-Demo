using Bookstore.Repositories;
using Bookstore.Services;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Extentions
{
    public static class ServiceExtentions
    {

        //EXTENTİONS
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=bookstore.db"));

        }


        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {

            services.AddScoped<IDemoRepository, DemoRepository>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IDemoService, DemoService>();
        }
    }
}
