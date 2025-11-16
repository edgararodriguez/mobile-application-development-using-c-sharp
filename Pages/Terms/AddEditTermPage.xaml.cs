using C971.ViewModels.Terms;

namespace C971.Pages.Terms;

public partial class AddEditTermPage : ContentPage
{
    public AddEditTermPage(int? termId = null)
    {
        InitializeComponent();
        BindingContext = new AddEditTermViewModel(termId);
    }
}
