using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using C971.Models;
using C971.Services;

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

        var filtered = _allCourses.Where(c =>
            CourseSearchService.Matches(
                c.Title,
                c.InstructorName,
                c.Status.ToString(),
                SearchText));

        foreach (var course in filtered)
            Courses.Add(course);
    }
}