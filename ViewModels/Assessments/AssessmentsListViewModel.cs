using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using C971.Models;

namespace C971.ViewModels.Assessments;

public partial class AssessmentsListViewModel : ObservableObject
{
    [ObservableProperty]
    private Course course;

    public ObservableCollection<Assessment> Assessments { get; } = new();

    public AssessmentsListViewModel(Course course)
    {
        Course = course;
    }

    [RelayCommand]
    public async Task LoadAssessmentsAsync()
    {
        if (Course?.Id <= 0)
            return;

        Assessments.Clear();

        var items = await App.Database.GetAssessmentsForCourseAsync(Course.Id);
        foreach (var a in items)
            Assessments.Add(a);
    }
}
