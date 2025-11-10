using C971.Data;
using C971.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDatabase _db;
    public CourseRepository(AppDatabase db) => _db = db;

    public Task<int> DeleteAsync(Course entity) => _db.Connection.DeleteAsync(entity);
    public Task<List<Course>> GetAllAsync() => _db.Connection.Table<Course>().ToListAsync();
    public Task<Course?> GetAsync(int id) => _db.Connection.FindAsync<Course>(id);
    public Task<int> InsertAsync(Course e) => _db.Connection.InsertAsync(e);
    public Task<int> UpdateAsync(Course e) => _db.Connection.UpdateAsync(e);

    public Task<List<Course>> GetByTermAsync(int termId) =>
        _db.Connection.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
}
