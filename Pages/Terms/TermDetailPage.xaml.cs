using C971.Models;

namespace C971.Pages.Terms;

public partial class TermDetailPage : ContentPage
{
    public TermDetailPage(Term term)
    {
        InitializeComponent();
        BindingContext = term; // read-only view of the selected term
    }

    // Stub for future navigation to course pages
    private async void OnManageCoursesClicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Courses",
            "This will navigate to the Course list/detail pages for this term once they are implemented.",
            "OK");
    }
}
