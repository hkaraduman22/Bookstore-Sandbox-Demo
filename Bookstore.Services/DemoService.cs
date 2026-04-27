using Bookstore.Entities;
using Bookstore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services;

public class DemoService : IDemoService
{
    private readonly AppDbContext _context;

    public DemoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task ResetSystemToDemoAsync()
    {
        _context.Books.RemoveRange(_context.Books);
        _context.Categories.RemoveRange(_context.Categories);
        await _context.SaveChangesAsync();
         
        var catYazilim = new Category { Name = "Yazılım & Teknoloji" };
        var catTarih = new Category { Name = "Tarih" };
        var catEdebiyat = new Category { Name = "Dünya Edebiyatı" };

        await _context.Categories.AddRangeAsync(catYazilim, catTarih, catEdebiyat);
        await _context.SaveChangesAsync(); // Kategoriler ID alsın diye önce kaydediyoruz

         
        var demoBooks = new List<Book>
        {
            new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 450.00m, CategoryId = catYazilim.Id },
            new Book { Title = "Design Patterns", Author = "GoF", Price = 520.50m, CategoryId = catYazilim.Id },
            new Book { Title = "Sapiens", Author = "Yuval Noah Harari", Price = 250.00m, CategoryId = catTarih.Id },
            new Book { Title = "1984", Author = "George Orwell", Price = 120.00m, CategoryId = catEdebiyat.Id },
            new Book { Title = "Suç ve Ceza", Author = "Dostoyevski", Price = 180.00m, CategoryId = catEdebiyat.Id }
        };

        await _context.Books.AddRangeAsync(demoBooks);
        await _context.SaveChangesAsync();
    }

    public async Task CreateTestingChaosAsync(int count)
    {
        // Rastgele kategori seçimi için var olan kategorileri çek
        var categories = await _context.Categories.ToListAsync();
        if (!categories.Any()) return; // Kategori yoksa kaos da yaratamayız

        var random = new Random();
        var chaosBooks = new List<Book>();

        for (int i = 0; i < count; i++)
        {
            var randomCategory = categories[random.Next(categories.Count)];
            chaosBooks.Add(new Book
            {
                Title = $"Bozuk Veri {Guid.NewGuid().ToString().Substring(0, 5)}",
                Author = "Bilinmeyen Hacker",
                Price = (decimal)(random.NextDouble() * 1000),
                CategoryId = randomCategory.Id
            });
        }

        await _context.Books.AddRangeAsync(chaosBooks);
        await _context.SaveChangesAsync();
    }
}