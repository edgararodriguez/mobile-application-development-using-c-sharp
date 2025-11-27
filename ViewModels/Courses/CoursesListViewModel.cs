using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using C971.Models;

namespace C971.ViewModels.Courses;

public partial class CoursesListViewModel : ObservableObject
{
    private readonly int _termId;

    [ObservableProperty]
    private Term? term;

    // Bindable list for the CollectionView
    public ObservableCollection<Course> Courses { get; } = new();

    public CoursesListViewModel(Term term)
    {
        _termId = term.Id;
        Term = term;
    }

    // This becomes LoadCoursesCommand for binding
    [RelayCommand]
    public async Task LoadCoursesAsync()
    {
        if (_termId <= 0)
            return;

        Courses.Clear();

        var items = await App.Database.GetCoursesForTermAsync(_termId);
        foreach (var c in items)
            Courses.Add(c);
    }
}
