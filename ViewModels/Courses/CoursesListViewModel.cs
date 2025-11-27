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

    public ObservableCollection<Course> Courses { get; } = new();

    public CoursesListViewModel(Term term)
    {
        Term = term;
    }

    [RelayCommand]
    public async Task LoadCoursesAsync()
    {
        if (Term?.Id <= 0)
            return;

        Courses.Clear();

        var items = await App.Database.GetCoursesForTermAsync(Term.Id);
        foreach (var c in items)
            Courses.Add(c);
    }
}
