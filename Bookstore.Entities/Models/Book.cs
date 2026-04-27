namespace Bookstore.Entities;

public class Book
{
    public int Id { get; set; } // Otomatik artan (Primary Key)
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public decimal Price { get; set; }
}