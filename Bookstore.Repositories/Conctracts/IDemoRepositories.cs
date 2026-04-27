namespace Bookstore.Repositories;

public interface IDemoRepository
{
    Task ClearDatabaseAsync();
    Task SeedInitialDataAsync();
    Task AddRandomGarbageDataAsync(int count);
}