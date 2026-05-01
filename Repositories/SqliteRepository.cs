using C971.Data;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public class SqliteRepository<TEntity> : IRepository<TEntity> where TEntity : new()
{
    protected readonly SQLiteAsyncConnection Connection;

    public SqliteRepository(AppDatabase db)
    {
        Connection = db.Connection;
    }

    public virtual Task<TEntity?> GetAsync(int id) =>
        Connection.FindAsync<TEntity>(id);

    public virtual Task<List<TEntity>> GetAllAsync() =>
        Connection.Table<TEntity>().ToListAsync();

    public virtual Task<int> InsertAsync(TEntity entity) =>
        Connection.InsertAsync(entity);

    public virtual Task<int> UpdateAsync(TEntity entity) =>
        Connection.UpdateAsync(entity);

    public virtual Task<int> DeleteAsync(TEntity entity) =>
        Connection.DeleteAsync(entity);
}