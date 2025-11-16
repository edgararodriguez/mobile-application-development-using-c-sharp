using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using C971.Models;
using Microsoft.Maui.Controls;

namespace C971.ViewModels.Terms
{
    public class AddEditTermViewModel : INotifyPropertyChanged
    {
        private Term _term = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public Term Term
        {
            get => _term;
            set
            {
                if (_term == value) return;
                _term = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        public string PageTitle => Term.Id > 0 ? "Edit Term" : "Add Term";

        // Status options for the picker (matches Term.Status)
        public IList<string> AvailableStatuses { get; } = new List<string>
        {
            "Planned",
            "In Progress",
            "Completed",
            "Dropped"
        };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditTermViewModel(int? termId = null)
        {
            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await CancelAsync());

            if (termId.HasValue)
            {
                _ = LoadExistingTermAsync(termId.Value);
            }
            else
            {
                Term = new Term
                {
                    Title = string.Empty,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(6),
                    Status = "Planned"
                };
            }
        }

        private async Task LoadExistingTermAsync(int id)
        {
            var term = await App.Database.GetTermAsync(id);
            if (term != null)
            {
                Term = term;
            }
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Term.Title))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "Term title is required.",
                    "OK");
                return;
            }

            if (Term.EndDate <= Term.StartDate)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "End date must be after the start date.",
                    "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Term.Status))
                Term.Status = "Planned";

            await App.Database.SaveTermAsync(Term);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task CancelAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
