using C971.Models;

namespace C971.Pages.Courses;

public partial class CourseDetailPage : ContentPage
{
    private readonly int _courseId;

    public CourseDetailPage(int courseId)
    {
        InitializeComponent();
        _courseId = courseId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var course = await App.Database.GetCourseAsync(_courseId);
        if (course != null)
        {
            BindingContext = course;
        }
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (BindingContext is Course course)
        {
            await Navigation.PushAsync(new AddEditCoursePage(course.TermId, course.Id));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (BindingContext is Course course)
        {
            var confirm = await DisplayAlert(
                "Delete Course",
                "Are you sure you want to delete this course?",
                "Delete",
                "Cancel");

            if (!confirm)
                return;

            await App.Database.DeleteCourseAsync(course);
            await Navigation.PopAsync();
        }
    }
}
