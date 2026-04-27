using Bookstore.Entities;

namespace Bookstore.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
    void CreateBook(Book book);  
}