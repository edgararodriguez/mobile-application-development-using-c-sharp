using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using C971.Models;
using Microsoft.Maui.Controls;

namespace C971.ViewModels.Assessments
{
    public class AddEditAssessmentViewModel : INotifyPropertyChanged
    {
        private Assessment _assessment = new();

        private readonly int _courseId;
        private readonly int? _assessmentId;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Assessment Assessment
        {
            get => _assessment;
            set
            {
                if (_assessment != value)
                {
                    _assessment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditAssessmentViewModel(int courseId, int? assessmentId = null)
        {
            _courseId = courseId;
            _assessmentId = assessmentId;

            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await CancelAsync());

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (_assessmentId.HasValue)
            {
                var existing = await App.Database.GetAssessmentAsync(_assessmentId.Value);
                if (existing != null)
                {
                    // Coerce null dates to something the DatePicker can handle
                    existing.StartDate ??= DateTime.Today;
                    existing.DueDate ??= DateTime.Today.AddDays(7);
                    Assessment = existing;
                    return;
                }
            }

            Assessment = new Assessment
            {
                CourseId = _courseId,
                Name = string.Empty,
                Type = AssessmentType.Objective,
                StartDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                NotifyOnStart = false,
                NotifyOnEnd = false
            };
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Assessment.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "Assessment name is required.",
                    "OK");
                return;
            }

            var start = Assessment.StartDate ?? DateTime.Today;
            var due = Assessment.DueDate ?? DateTime.Today;

            if (due <= start)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "Due date must be after the start date.",
                    "OK");
                return;
            }

            Assessment.StartDate = start;
            Assessment.DueDate = due;

            await App.Database.SaveAssessmentAsync(Assessment);

            // ScheduleAssessmentNotifications(Assessment);

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
