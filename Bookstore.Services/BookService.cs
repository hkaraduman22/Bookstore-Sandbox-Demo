using Bookstore.Entities;
using Bookstore.Repositories;

namespace Bookstore.Services;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repository;
    public BookService(IRepositoryManager repository) => _repository = repository;

    public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
        await _repository.Book.GetAllBooksAsync(trackChanges);

    public async Task<Book> CreateOneBookAsync(Book book)
    {
        _repository.Book.CreateBook(book);
        await _repository.SaveAsync(); // Veriyi fiziksel olarak DB'ye yazar.
        return book;
    }
}