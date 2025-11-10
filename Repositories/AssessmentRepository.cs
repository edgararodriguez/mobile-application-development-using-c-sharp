using C971.Data;
using C971.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public class AssessmentRepository : IAssessmentRepository
{
    private readonly AppDatabase _db;
    public AssessmentRepository(AppDatabase db) => _db = db;

    public Task<int> DeleteAsync(Assessment entity) => _db.Connection.DeleteAsync(entity);
    public Task<List<Assessment>> GetAllAsync() => _db.Connection.Table<Assessment>().ToListAsync();
    public Task<Assessment?> GetAsync(int id) => _db.Connection.FindAsync<Assessment>(id);
    public Task<int> InsertAsync(Assessment e) => _db.Connection.InsertAsync(e);
    public Task<int> UpdateAsync(Assessment e) => _db.Connection.UpdateAsync(e);

    public Task<List<Assessment>> GetByCourseAsync(int courseId) =>
        _db.Connection.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();
}
