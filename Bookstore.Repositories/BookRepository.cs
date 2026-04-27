using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Repositories;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    
    public BookRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
        await FindAll(trackChanges).ToListAsync();

    public void CreateBook(Book book) => Create(book);
}