 
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Bu iki satır hayati önem taşıyor, tabloları temsil eder
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
}