using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using C971.Models;
using C971.Services;
using Microsoft.Maui.Controls;

namespace C971.ViewModels.Courses
{
    public class AddEditCourseViewModel : INotifyPropertyChanged
    {
        private Course _course = new();

        private readonly int _termId;
        private readonly int? _courseId;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Course Course
        {
            get => _course;
            set
            {
                if (_course != value)
                {
                    _course = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddEditCourseViewModel(int termId, int? courseId = null)
        {
            _termId = termId;
            _courseId = courseId;

            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await CancelAsync());

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (_courseId.HasValue)
            {
                var existing = await App.Database.GetCourseAsync(_courseId.Value);
                if (existing != null)
                {
                    Course = existing;
                    return;
                }
            }

            // New course default values
            Course = new Course
            {
                TermId = _termId,
                Title = string.Empty,
                Status = CourseStatus.PlanToTake,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1)
            };
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Course.Title))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "Course title is required.",
                    "OK");
                return;
            }

            if (Course.EndDate <= Course.StartDate)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "End date must be after the start date.",
                    "OK");
                return;
            }

            if (!string.IsNullOrWhiteSpace(Course.InstructorEmail) &&
                !Course.InstructorEmail.Contains("@"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation Error",
                    "Please enter a valid instructor email address.",
                    "OK");
                return;
            }

            await App.Database.SaveCourseAsync(Course);

            // Schedule alerts for this course (C4 rubric)
            await NotificationService.ScheduleCourseNotificationsAsync(Course);

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
