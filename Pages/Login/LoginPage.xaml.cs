using C971.Pages.Courses;

namespace C971.Pages;

public partial class LoginPage : ContentPage
{
    private const string ValidUsername = "student";
    private const string ValidPassword = "wgu123";

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var username = UsernameEntry.Text?.Trim() ?? string.Empty;
        var password = PasswordEntry.Text ?? string.Empty;

        if (username == ValidUsername && password == ValidPassword)
        {
            Application.Current!.MainPage = new AppShell();
            return;
        }

        ErrorLabel.IsVisible = true;
        PasswordEntry.Text = string.Empty;
        await DisplayAlert("Login Failed", "Invalid username or password.", "OK");
    }
}