using SQLite;
using System;

namespace C971.Models;

public enum AssessmentType { Objective, Performance }

[Table("Assessments")]
public class Assessment
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }

    [Indexed, NotNull] public int CourseId { get; set; }

    [NotNull] public AssessmentType Type { get; set; } = AssessmentType.Objective;

    [MaxLength(120), NotNull] public string Name { get; set; } = "";

    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }

    public bool NotifyOnStart { get; set; }
    public bool NotifyOnEnd { get; set; }
}
