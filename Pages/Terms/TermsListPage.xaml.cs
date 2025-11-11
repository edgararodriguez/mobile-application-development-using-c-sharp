using C971.ViewModels.Terms;

namespace C971.Pages.Terms;

public partial class TermsListPage : ContentPage
{
    // DI will inject the VM because you registered it in MauiProgram
    public TermsListPage(TermsListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        // optionally auto-load
        // _ = vm.LoadTermsAsync();
    }
}
