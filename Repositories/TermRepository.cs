using C971.Data;
using C971.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public class TermRepository : ITermRepository
{
    private readonly AppDatabase _db;
    public TermRepository(AppDatabase db) => _db = db;

    public Task<int> DeleteAsync(Term entity) => _db.Connection.DeleteAsync(entity);
    public Task<List<Term>> GetAllAsync() => _db.Connection.Table<Term>().ToListAsync();
    public Task<Term?> GetAsync(int id) => _db.Connection.FindAsync<Term>(id);
    public Task<int> InsertAsync(Term entity) => _db.Connection.InsertAsync(entity);
    public Task<int> UpdateAsync(Term entity) => _db.Connection.UpdateAsync(entity);
}
