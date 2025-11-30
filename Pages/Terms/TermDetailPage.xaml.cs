using C971.Models;
using C971.Pages.Courses;

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
        if (BindingContext is Term term)
        {
            // Navigate to the C1 Courses list for this term
            await Navigation.PushAsync(new CoursesListPage(term));
        }
    }

    private async void OnEditTermClicked(object sender, EventArgs e)
    {
        if (BindingContext is Term t)
        {
            await Navigation.PushAsync(new AddEditTermPage(t.Id));
        }
    }

    private async void OnDeleteTermClicked(object sender, EventArgs e)
    {
        if (BindingContext is not Term term)
            return;

        bool confirm = await DisplayAlert(
            "Delete Term",
            "Are you sure you want to delete this term? This action cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirm)
            return;

        // Delete from DB
        await App.Database.DeleteTermAsync(term);

        // Navigate back
        await Navigation.PopAsync();
    }
}
