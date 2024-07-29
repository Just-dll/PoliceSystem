using BLL.Models;

namespace BLL.Interfaces;

public interface IService<T> where T : BaseModel
{
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(int id);
    Task DeleteAsync(T entity);
}