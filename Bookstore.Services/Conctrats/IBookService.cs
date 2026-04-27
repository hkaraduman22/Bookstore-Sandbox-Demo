using Bookstore.Entities;

namespace Bookstore.Services;

public interface IBookService
{ 
    Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
   
    Task<Book> CreateOneBookAsync(Book book);
}