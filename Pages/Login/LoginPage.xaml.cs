using C971.Services;

namespace C971.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (LoginValidator.IsValid(username, password))
        {
            Application.Current!.MainPage = new AppShell();
            return;
        }

        ErrorLabel.IsVisible = true;
        PasswordEntry.Text = string.Empty;
        await DisplayAlert("Login Failed", "Invalid username or password.", "OK");
    }
}