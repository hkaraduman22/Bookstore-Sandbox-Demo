using Bookstore.Repositories;

namespace Bookstore.Services;

public class DemoService : IDemoService
{
    private readonly IDemoRepository _repository;

    public DemoService(IDemoRepository repository)
    {
        _repository = repository;
    }

    public async Task ResetSystemToDemoAsync()
    {
        // İş mantığı: Önce temizle, sonra temiz verileri bas
        await _repository.ClearDatabaseAsync();
        await _repository.SeedInitialDataAsync();
    }

    public async Task CreateTestingChaosAsync(int count)
    {
        // Sistemi sınamak için çöp veri ekleme görevini repo'ya ilet
        await _repository.AddRandomGarbageDataAsync(count);
    }

    
}