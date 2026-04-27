namespace Bookstore.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;





    //N-M
    public ICollection<Book> Books { get; set; } = new List<Book>();
}