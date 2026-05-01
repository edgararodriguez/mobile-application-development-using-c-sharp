using C971.Data;
using C971.Models;

namespace C971.Repositories;

public class CourseRepository : SqliteRepository<Course>, ICourseRepository
{
    public CourseRepository(AppDatabase db) : base(db)
    {
    }

    public Task<List<Course>> GetByTermAsync(int termId) =>
        Connection.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
}