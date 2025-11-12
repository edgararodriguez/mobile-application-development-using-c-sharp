using C971.ViewModels.Terms;

namespace C971.Pages.Terms;

public partial class TermsListPage : ContentPage
{
    public TermsListPage(TermsListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TermsListViewModel vm)
        {
            // avoid double-loads if you like:
            if (vm.Terms.Count == 0)
                await vm.LoadTermsAsync();
        }
    }
}
