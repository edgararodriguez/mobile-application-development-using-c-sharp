using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using C971.Models;

namespace C971.ViewModels.Courses;

public partial class CoursesListViewModel : ObservableObject
{
    [ObservableProperty]
    private Term term;

    [ObservableProperty]
    private string searchText = string.Empty;
    public ObservableCollection<Course> Courses { get; } = new();

    private readonly List<Course> _allCourses = new();
    public CoursesListViewModel(Term term)
    {
        Term = term;
    }

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilter();
    }
    [RelayCommand]
    public async Task LoadCoursesAsync()
    {
        if (Term?.Id <= 0)
            return;

        _allCourses.Clear();
        Courses.Clear();

        var items = await App.Database.GetCoursesForTermAsync(Term.Id);
        foreach (var c in items)
            _allCourses.Add(c);

        ApplyFilter();
    }
    private void ApplyFilter()
    {
        Courses.Clear();

        IEnumerable<Course> filtered = _allCourses;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var query = SearchText.Trim().ToLowerInvariant();

            filtered = _allCourses.Where(c =>
                (!string.IsNullOrWhiteSpace(c.Title) && c.Title.ToLowerInvariant().Contains(query)) ||
                (!string.IsNullOrWhiteSpace(c.InstructorName) && c.InstructorName.ToLowerInvariant().Contains(query)) ||
                c.Status.ToString().ToLowerInvariant().Contains(query)
            );
        }

        foreach (var course in filtered)
            Courses.Add(course);
    }
}