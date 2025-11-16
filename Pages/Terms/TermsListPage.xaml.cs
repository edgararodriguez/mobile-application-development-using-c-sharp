using System.Linq;
using C971.Models;
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
            // Always reload terms to show updated items
            await vm.LoadTermsAsync();
        }
    }


    private async void OnTermSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Term selectedTerm)
            return;

        await Navigation.PushAsync(new TermDetailPage(selectedTerm));

        if (sender is CollectionView cv)
            cv.SelectedItem = null;
    }

    private async void OnAddTermClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEditTermPage());
    }
}
