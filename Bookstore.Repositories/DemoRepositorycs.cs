
using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories;

public class DemoRepository : IDemoRepository
{
    private readonly AppDbContext _context;

    public DemoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task ClearDatabaseAsync()
    { 
        // Tüm kitapları ve kategorileri bulup veritabanından siliyoruz.
        _context.Books.RemoveRange(_context.Books);
        _context.Categories.RemoveRange(_context.Categories);

        await _context.SaveChangesAsync();
    }

    public async Task SeedInitialDataAsync()
    {
        // İçeride zaten veri varsa, tekrar üstüne ekleme yapmamak için kontrol ediyoruz
        if (await _context.Categories.AnyAsync()) return;

        // 1. Tertemiz "Pazarlama Demosu" Kategorileri
        var category1 = new Category { Name = "Bilim Kurgu & Fantastik" };
        var category2 = new Category { Name = "Kişisel Gelişim" };

        // 2. Vitrinlik, profesyonel duran kitaplar
        var books = new List<Book>
        {
            new Book { Title = "Dune", Author = "Frank Herbert", Price = 250.50m, Category = category1 },
            new Book { Title = "Vakıf", Author = "Isaac Asimov", Price = 180.00m, Category = category1 },
            new Book { Title = "Yüzüklerin Efendisi", Author = "J.R.R. Tolkien", Price = 450.00m, Category = category1 },
            new Book { Title = "Atomik Alışkanlıklar", Author = "James Clear", Price = 120.00m, Category = category2 },
            new Book { Title = "İnsanın Anlam Arayışı", Author = "Viktor E. Frankl", Price = 90.00m, Category = category2 }
        };

        // Veritabanına ekle ve kaydet
        await _context.Categories.AddRangeAsync(category1, category2);
        await _context.Books.AddRangeAsync(books);

        await _context.SaveChangesAsync();
    }

    public async Task AddRandomGarbageDataAsync(int count)
    {
        // Sistemi sınamak için garbage veriler
        var category = new Category { Name = "!!! TEST KATEGORİSİ !!!" };
        var garbageBooks = new List<Book>();

        for (int i = 0; i < count; i++)
        {
            garbageBooks.Add(new Book
            {
                Title = $"ASDFG Bozuk Kitap {Guid.NewGuid().ToString().Substring(0, 5)}",
                Author = "Bilinmeyen Yazar",
                Price = -99.99m, // Kasten mantıksız bir fiyat (-99) giriyoruz
                Category = category
            });
        }

        await _context.Categories.AddAsync(category);
        await _context.Books.AddRangeAsync(garbageBooks);
        await _context.SaveChangesAsync();
    }
}