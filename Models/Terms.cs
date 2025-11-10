using SQLite;
using System;

namespace C971.Models;

[Table("Terms")]
public class Term
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [MaxLength(100), NotNull] public string Title { get; set; } = "";
    [NotNull] public DateTime StartDate { get; set; }
    [NotNull] public DateTime EndDate { get; set; }
}
