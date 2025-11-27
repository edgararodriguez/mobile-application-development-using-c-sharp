using System.Linq;
using C971.Models;
using C971.ViewModels.Courses;

namespace C971.Pages.Courses;

public partial class CoursesListPage : ContentPage
{
    private readonly CoursesListViewModel _viewModel;

    public CoursesListPage(Term term)
    {
        InitializeComponent();

        _viewModel = new CoursesListViewModel(term);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.LoadCoursesCommand.CanExecute(null))
        {
            _viewModel.LoadCoursesCommand.Execute(null);
        }
    }

    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEditCoursePage(_viewModel.Term.Id));
    }

    private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Course selected)
        {
            // Clear selection so tapping again works
            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushAsync(new CourseDetailPage(selected.Id));
        }
    }
}
