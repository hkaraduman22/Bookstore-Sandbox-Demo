using System.Linq.Expressions;

namespace Bookstore.Repositories;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    void Create(T entity); // Alt yapının Create sözleşmesi
}