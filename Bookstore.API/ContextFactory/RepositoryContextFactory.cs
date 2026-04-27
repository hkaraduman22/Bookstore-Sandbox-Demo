using Bookstore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookstore.API.ContextFactory;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // appsettings.json dosyasını okuyabilmek için konfigürasyon inşa ediyoruz
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // SQL Server DEĞİL, SQLite kullanıyoruz.
        // Migration dosyalarının Bookstore.API içinde oluşmasını istiyoruz.
        var builder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(configuration.GetConnectionString("sqlConnection"),
                prj => prj.MigrationsAssembly("Bookstore.API"));

        // HATA DÜZELTİLDİ: RepositoryContext değil, senin DB sınıfın olan AppDbContext dönüyoruz.
        return new AppDbContext(builder.Options);
    }
}