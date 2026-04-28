using Bookstore.Entities;
using Bookstore.Repositories;
using Bookstore.Services.Conctrats;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bookstore.Services;

public class DemoService : IDemoService
{

    private readonly AppDbContext _context;

    private readonly ILoggingService _logger;
    public DemoService(AppDbContext context,ILoggingService logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task ResetSystemToDemoAsync()
    { 
        _context.Books.RemoveRange(_context.Books);
        _context.Categories.RemoveRange(_context.Categories);
        await _context.SaveChangesAsync();


        //QUERY WİTHOUT LİNQ 
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Categories';");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name='Books';");
         
        var catYazilim = new Category { Name = "Yazılım & Teknoloji" };
        var catTarih = new Category { Name = "Tarih" };
        var catEdebiyat = new Category { Name = "Dünya Edebiyatı" };

        await _context.Categories.AddRangeAsync(catYazilim, catTarih, catEdebiyat);
        await _context.SaveChangesAsync();  
         
        var demoBooks = new List<Book>
        {
            new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 450.00m, CategoryId = catYazilim.Id },
            new Book { Title = "Design Patterns", Author = "GoF", Price = 520.50m, CategoryId = catYazilim.Id },
            new Book { Title = "Sapiens", Author = "Yuval Noah Harari", Price = 250.00m, CategoryId = catTarih.Id },
            new Book { Title = "1984", Author = "George Orwell", Price = 120.00m, CategoryId = catEdebiyat.Id },
            new Book { Title = "Suç ve Ceza", Author = "Dostoyevski", Price = 180.00m, CategoryId = catEdebiyat.Id }
        };
        string message = "The system was restored to its default state!";
        _logger.LogInfo(message);

        await _context.Books.AddRangeAsync(demoBooks);
        await _context.SaveChangesAsync();
    }

   
}