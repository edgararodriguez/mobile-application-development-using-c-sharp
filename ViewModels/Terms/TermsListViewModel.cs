using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using C971.Models;
using C971.Repositories;

namespace C971.ViewModels.Terms;

public partial class TermsListViewModel : ObservableObject
{
    private readonly ITermRepository _termRepository;

    // Bindable list for the CollectionView
    public ObservableCollection<Term> Terms { get; } = new();

    public TermsListViewModel(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    [RelayCommand]
    public async Task LoadTermsAsync()
    {
        // Avoid re-entrant loads if you add IsBusy later
        Terms.Clear();

        var items = await _termRepository.GetAllAsync();
        foreach (var t in items)
            Terms.Add(t);
    }
}
