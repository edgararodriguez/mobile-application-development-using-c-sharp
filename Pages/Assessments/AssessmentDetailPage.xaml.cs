using C971.Models;

namespace C971.Pages.Assessments;

public partial class AssessmentDetailPage : ContentPage
{
    private readonly int _assessmentId;

    public AssessmentDetailPage(int assessmentId)
    {
        InitializeComponent();
        _assessmentId = assessmentId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var assessment = await App.Database.GetAssessmentAsync(_assessmentId);
        if (assessment != null)
        {
            // ensure dates are non-null for display
            assessment.StartDate ??= DateTime.Today;
            assessment.DueDate ??= DateTime.Today.AddDays(7);
            BindingContext = assessment;
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (BindingContext is Assessment a)
        {
            await Navigation.PushAsync(new AddEditAssessmentPage(a.CourseId, a.Id));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (BindingContext is Assessment a)
        {
            var confirm = await DisplayAlert(
                "Delete Assessment",
                "Are you sure you want to delete this assessment?",
                "Delete",
                "Cancel");

            if (!confirm)
                return;

            await App.Database.DeleteAssessmentAsync(a);
            await Navigation.PopAsync();
        }
    }
}
