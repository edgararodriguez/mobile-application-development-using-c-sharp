using C971.ViewModels.Courses;

namespace C971.Pages.Courses;

public partial class AddEditCoursePage : ContentPage
{
    public AddEditCoursePage(int termId, int? courseId = null)
    {
        InitializeComponent();
        BindingContext = new AddEditCourseViewModel(termId, courseId);
    }
}
