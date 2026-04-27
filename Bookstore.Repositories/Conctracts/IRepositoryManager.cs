namespace Bookstore.Repositories;

public interface IRepositoryManager
{
    IBookRepository Book { get; }
    Task SaveAsync();
}