using SQLite;
using System.IO;
using System.Threading.Tasks;

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
        Connection.CreateTableAsync<Models.Term>()
        .ContinueWith(_ => Connection.CreateTableAsync<Models.Course>()).Unwrap()
        .ContinueWith(_ => Connection.CreateTableAsync<Models.Assessment>()).Unwrap();
}
