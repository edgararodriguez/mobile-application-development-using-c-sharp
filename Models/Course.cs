using SQLite;
using System;

namespace C971.Models;

public enum CourseStatus { InProgress, Completed, Dropped, PlanToTake }

[Table("Courses")]
public class Course
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }

    [Indexed, NotNull] public int TermId { get; set; }

    [MaxLength(120), NotNull] public string Title { get; set; } = "";

    [NotNull] public CourseStatus Status { get; set; } = CourseStatus.PlanToTake;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Instructor
    [MaxLength(80)] public string? InstructorName { get; set; }
    [MaxLength(30)] public string? InstructorPhone { get; set; }
    [MaxLength(120)] public string? InstructorEmail { get; set; }

    // Optional notes (you can share through OS sheet)
    public string? Notes { get; set; }

    // Alerts
    public bool NotifyOnStart { get; set; }
    public bool NotifyOnEnd { get; set; }
}
