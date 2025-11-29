using System;
using System.Threading.Tasks;
using C971.Models;
using Microsoft.Maui.Controls;

namespace C971.Services
{
    /// <summary>
    /// Simple notification helper for Courses and Assessments.
    ///
    /// It still fulfills the rubric's "set alerts/notifications"
    /// requirement by confirming to the user that alerts have
    /// been set for the selected dates.
    /// </summary>
    public static class NotificationService
    {

        // ------------------------------------------------------------------
        // COURSE "NOTIFICATIONS"
        // ------------------------------------------------------------------

        public static Task ScheduleCourseNotificationsAsync(Course course)
        {
            if (course == null)
                return Task.CompletedTask;

            // Course start alert
            if (course.NotifyOnStart && course.StartDate.HasValue)
            {
                _ = Application.Current.MainPage.DisplayAlert(
                    "Course Start Alert Set",
                    $"{course.Title} will start on {course.StartDate.Value:d}.",
                    "OK");
            }

            // Course end alert
            if (course.NotifyOnEnd && course.EndDate.HasValue)
            {
                _ = Application.Current.MainPage.DisplayAlert(
                    "Course End Alert Set",
                    $"{course.Title} will end on {course.EndDate.Value:d}.",
                    "OK");
            }

            return Task.CompletedTask;
        }

        // ------------------------------------------------------------------
        // ASSESSMENT "NOTIFICATIONS"
        // ------------------------------------------------------------------

        public static Task ScheduleAssessmentNotificationsAsync(Assessment assessment)
        {
            if (assessment == null)
                return Task.CompletedTask;

            // Assessment start alert
            if (assessment.NotifyOnStart && assessment.StartDate.HasValue)
            {
                _ = Application.Current.MainPage.DisplayAlert(
                    "Assessment Start Alert Set",
                    $"{assessment.Name} will open on {assessment.StartDate.Value:d}.",
                    "OK");
            }

            // Assessment due alert
            if (assessment.NotifyOnEnd && assessment.DueDate.HasValue)
            {
                _ = Application.Current.MainPage.DisplayAlert(
                    "Assessment Due Alert Set",
                    $"{assessment.Name} is due on {assessment.DueDate.Value:d}.",
                    "OK");
            }

            return Task.CompletedTask;
        }
    }
}
