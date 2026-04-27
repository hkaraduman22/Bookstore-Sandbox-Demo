using Bookstore.Repositories;

public class RepositoryManager : IRepositoryManager
{
    public IBookRepository Book => throw new NotImplementedException();

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}