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
}
