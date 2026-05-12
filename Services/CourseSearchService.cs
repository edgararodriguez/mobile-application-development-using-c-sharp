namespace C971.Services;

public static class CourseSearchService
{
    public static bool Matches(string? title, string? instructorName, string? status, string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return true;

        var query = searchText.Trim().ToLowerInvariant();

        return (!string.IsNullOrWhiteSpace(title) &&
                title.ToLowerInvariant().Contains(query)) ||
               (!string.IsNullOrWhiteSpace(instructorName) &&
                instructorName.ToLowerInvariant().Contains(query)) ||
               (!string.IsNullOrWhiteSpace(status) &&
                status.ToLowerInvariant().Contains(query));
    }
}