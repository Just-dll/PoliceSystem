using DAL.Entities;
using System.Linq.Expressions;

namespace DAL.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
