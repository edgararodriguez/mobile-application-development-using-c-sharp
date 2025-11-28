using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C971.Models;
using SQLite;

namespace C971.Data
{

    public class AppDatabase
    {
        public SQLiteAsyncConnection Connection { get; }

        public AppDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "c971.db3");
            Connection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitAsync()
        {
            await Connection.CreateTableAsync<Term>();
            await Connection.CreateTableAsync<Course>();
            await Connection.CreateTableAsync<Assessment>();
        }

        // ------------------ TERM CRUD ------------------

        public Task<List<Term>> GetTermsAsync() =>
            Connection.Table<Term>()
                      .OrderBy(t => t.StartDate)
                      .ToListAsync();

        public Task<Term?> GetTermAsync(int id) =>
            Connection.Table<Term>()
                      .FirstOrDefaultAsync(t => t.Id == id);

        public Task<int> SaveTermAsync(Term term) =>
            term.Id == 0
                ? Connection.InsertAsync(term)
                : Connection.UpdateAsync(term);

        public Task<int> DeleteTermAsync(Term term) =>
            Connection.DeleteAsync(term);

        // ------------------ COURSE CRUD ------------------

        public Task<List<Course>> GetCoursesForTermAsync(int termId) =>
            Connection.Table<Course>()
                      .Where(c => c.TermId == termId)
                      .OrderBy(c => c.StartDate)
                      .ToListAsync();

        public Task<Course?> GetCourseAsync(int id) =>
            Connection.Table<Course>()
                      .FirstOrDefaultAsync(c => c.Id == id);

        public Task<int> SaveCourseAsync(Course course) =>
            course.Id == 0
                ? Connection.InsertAsync(course)
                : Connection.UpdateAsync(course);

        public Task<int> DeleteCourseAsync(Course course) =>
            Connection.DeleteAsync(course);

        // ------------------ ASSESSMENT CRUD ------------------

        public Task<List<Assessment>> GetAssessmentsForCourseAsync(int courseId) =>
            Connection.Table<Assessment>()
                      .Where(a => a.CourseId == courseId)
                      .OrderBy(a => a.StartDate)
                      .ToListAsync();

        public Task<Assessment?> GetAssessmentAsync(int id) =>
            Connection.Table<Assessment>()
                      .FirstOrDefaultAsync(a => a.Id == id);

        public Task<int> SaveAssessmentAsync(Assessment assessment) =>
            assessment.Id == 0
                ? Connection.InsertAsync(assessment)
                : Connection.UpdateAsync(assessment);

        public Task<int> DeleteAssessmentAsync(Assessment assessment) =>
            Connection.DeleteAsync(assessment);
    }
}
