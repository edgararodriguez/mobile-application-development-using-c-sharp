using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public interface IRepository<T>
{
    Task<T?> GetAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<int> InsertAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);
}
