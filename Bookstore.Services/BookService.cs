using Bookstore.Entities;
using Bookstore.Repositories;
using Bookstore.Services.Conctrats;

namespace Bookstore.Services;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repository;
    private ILoggingService _logger;
    public BookService(IRepositoryManager repository, ILoggingService logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
        await _repository.Book.GetAllBooksAsync(trackChanges);

    public async Task<Book> CreateOneBookAsync(Book book)
    {
        _repository.Book.CreateBook(book);
        string message =$"The book with Title:{book.Title} has been created!";
        _logger.LogInfo(message);
        await _repository.SaveAsync(); //SAVİNG


        return book;

    }
}