using System;
using System.Threading.Tasks;
using SQLite;                 // for Table<T>(), CountAsync(), InsertAsync
using C971.Data;             // AppDatabase
using C971.Models;           // Term, Course, Assessment, CourseStatus, AssessmentType

namespace C971.Data
{
    /// <summary>
    /// Seeds the demo dataset required by rubric section C6.
    /// Inserts exactly:
    ///  - 1 Term
    ///  - 1 Course in that Term
    ///  - 2 Assessments (Objective + Performance) for that Course
    ///  - Instructor: Anika Patel, 555-123-4567, anika.patel@strimeuniversity.edu
    /// Runs only when tables are empty to avoid duplicate rows.
    /// </summary>
    public class DemoDataSeeder
    {
        private readonly AppDatabase _db;

        public DemoDataSeeder(AppDatabase db)
        {
            _db = db;
        }

        public async Task SeedAsync()
        {
            // Adjust this to your actual connection property on AppDatabase
            var conn = _db.Connection;

            // Ensure tables exist (no-op if already created)
            await conn.CreateTableAsync<Term>();
            await conn.CreateTableAsync<Course>();
            await conn.CreateTableAsync<Assessment>();

            // Only seed if there is no data yet
            var termCount = await conn.Table<Term>().CountAsync();
            if (termCount > 0) return;

            // --- TERM --------------------------------------------------------
            var term = new Term
            {
             
                Title = "Spring Term",
                StartDate = DateTime.Today.AddDays(-30),
                EndDate = DateTime.Today.AddDays(60)
            };
            await conn.InsertAsync(term);

            // --- COURSE ------------------------------------------------------
            var course = new Course
            {
                TermId = term.Id,
             
                Title = "Data Structures",
                StartDate = DateTime.Today.AddDays(-14),
                EndDate = DateTime.Today.AddDays(45),
                Status = CourseStatus.InProgress,   // enum, not string
                InstructorName = "Anika Patel",
                InstructorPhone = "555-123-4567",
                InstructorEmail = "anika.patel@strimeuniversity.edu",
                Notes = "Seeded sample course for evaluation."
            };
            await conn.InsertAsync(course);

            // Stagger assessment dates relative to the course (handle DateTime? with fallbacks)
            var start = course.StartDate ?? DateTime.Today;
            var end = course.EndDate ?? DateTime.Today.AddDays(30);

            var objStart = start.AddDays(14);
            var objDue = end.AddDays(-14);
            var perfStart = start.AddDays(21);
            var perfDue = end.AddDays(-7);

            // --- ASSESSMENTS -------------------------------------------------
            var objective = new Assessment
            {
                CourseId = course.Id,
                Name = "Objective Assessment",            // Assessment uses Name (not Title)
                Type = AssessmentType.Objective,          // enum, not string
                StartDate = objStart,
                DueDate = objDue
            };

            var performance = new Assessment
            {
                CourseId = course.Id,
                Name = "Performance Assessment",
                Type = AssessmentType.Performance,
                StartDate = perfStart,
                DueDate = perfDue
            };

            await conn.InsertAsync(objective);
            await conn.InsertAsync(performance);
        }
    }
}
