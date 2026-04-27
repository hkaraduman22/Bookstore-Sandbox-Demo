using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookstore.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly AppDbContext _context;  

    public RepositoryBase(AppDbContext context) {
        _context = context;
    }

    public IQueryable<T> FindAll(bool trackChanges) =>
        !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

    public void Create(T entity) => _context.Set<T>().Add(entity);
}