using C971.ViewModels.Assessments;

namespace C971.Pages.Assessments;

public partial class AddEditAssessmentPage : ContentPage
{
    public AddEditAssessmentPage(int courseId, int? assessmentId = null)
    {
        InitializeComponent();
        BindingContext = new AddEditAssessmentViewModel(courseId, assessmentId);
    }
}
