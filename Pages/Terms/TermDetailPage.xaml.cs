using C971.Models;

namespace C971.Pages.Terms;

public partial class TermDetailPage : ContentPage
{
    public TermDetailPage(Term term)
    {
        InitializeComponent();
        BindingContext = term;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Refresh the term from the database so edits are shown
        if (BindingContext is Term term)
        {
            var refreshed = await App.Database.GetTermAsync(term.Id);
            if (refreshed != null)
            {
                BindingContext = refreshed;
            }
        }
    }

    private async void OnManageCoursesClicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Courses",
            "This will navigate to the Course list/detail pages for this term once they are implemented.",
            "OK");
    }

    private async void OnEditTermClicked(object sender, EventArgs e)
    {
        if (BindingContext is Term t)
        {
            await Navigation.PushAsync(new AddEditTermPage(t.Id));
        }
    }
}
