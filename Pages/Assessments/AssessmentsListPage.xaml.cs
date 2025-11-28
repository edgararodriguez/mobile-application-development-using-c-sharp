using System.Linq;
using C971.Models;
using C971.ViewModels.Assessments;

namespace C971.Pages.Assessments;

public partial class AssessmentsListPage : ContentPage
{
    private readonly AssessmentsListViewModel _viewModel;

    public AssessmentsListPage(Course course)
    {
        InitializeComponent();
        _viewModel = new AssessmentsListViewModel(course);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.LoadAssessmentsCommand.CanExecute(null))
        {
            _viewModel.LoadAssessmentsCommand.Execute(null);
        }
    }

    private async void OnAddAssessmentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEditAssessmentPage(_viewModel.Course.Id));
    }

    private async void OnAssessmentSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Assessment selected)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new AssessmentDetailPage(selected.Id));
        }
    }
}
