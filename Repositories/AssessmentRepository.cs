using C971.Data;
using C971.Models;

namespace C971.Repositories;

public class AssessmentRepository : SqliteRepository<Assessment>, IAssessmentRepository
{
    public AssessmentRepository(AppDatabase db) : base(db)
    {
    }

    public Task<List<Assessment>> GetByCourseAsync(int courseId) =>
        Connection.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();
}