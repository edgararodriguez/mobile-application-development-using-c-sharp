using C971.Data;
using C971.Models;

namespace C971.Repositories;

public class TermRepository : SqliteRepository<Term>, ITermRepository
{
    public TermRepository(AppDatabase db) : base(db)
    {
    }
}