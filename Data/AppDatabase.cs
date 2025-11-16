using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C971.Models;
using SQLite;

namespace C971.Data;

public class AppDatabase
{
    public SQLiteAsyncConnection Connection { get; }

    public AppDatabase()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "c971.db3");
        Connection = new SQLiteAsyncConnection(dbPath);
    }

    public Task InitAsync() =>
        Connection.CreateTableAsync<Term>()
        .ContinueWith(_ => Connection.CreateTableAsync<Course>()).Unwrap()
        .ContinueWith(_ => Connection.CreateTableAsync<Assessment>()).Unwrap();

    // ---------- TERM CRUD (used by ViewModels) ----------

    public Task<List<Term>> GetTermsAsync() =>
        Connection.Table<Term>()
                  .OrderBy(t => t.StartDate)
                  .ToListAsync();

    public Task<Term?> GetTermAsync(int id) =>
        Connection.Table<Term>()
                  .FirstOrDefaultAsync(t => t.Id == id);

    public Task<int> SaveTermAsync(Term term)
    {
        if (term.Id == 0)
        {
            return Connection.InsertAsync(term);
        }
        else
        {
            return Connection.UpdateAsync(term);
        }
    }

    public Task<int> DeleteTermAsync(Term term) =>
        Connection.DeleteAsync(term);
}
