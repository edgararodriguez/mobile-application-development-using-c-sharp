using C971.Models;

namespace C971.Pages.Courses;

public class CourseReportRow
{
    public string Title { get; set; } = "";
    public string Status { get; set; } = "";
    public string Instructor { get; set; } = "";
    public string StartDate { get; set; } = "";
    public string EndDate { get; set; } = "";
}

public class CourseReportViewModel
{
    public string ReportTitle { get; set; } = "Course Report";
    public string GeneratedOn { get; set; } = $"Generated on: {DateTime.Now:MM/dd/yyyy hh:mm tt}";
    public List<CourseReportRow> Rows { get; set; } = new();
}

public partial class CourseReportPage : ContentPage
{
    public CourseReportPage(Term term, IEnumerable<Course> courses)
    {
        Title = "Course Report";

        var viewModel = new CourseReportViewModel
        {
            ReportTitle = $"{term.Title} Course Report",
            GeneratedOn = $"Generated on: {DateTime.Now:MM/dd/yyyy hh:mm tt}",
            Rows = courses.Select(c => new CourseReportRow
            {
                Title = c.Title,
                Status = c.Status.ToString(),
                Instructor = string.IsNullOrWhiteSpace(c.InstructorName) ? "N/A" : c.InstructorName,
                StartDate = c.StartDate?.ToString("MM/dd/yyyy") ?? "N/A",
                EndDate = c.EndDate?.ToString("MM/dd/yyyy") ?? "N/A"
            }).ToList()
        };

        BindingContext = viewModel;

        var titleHeader = new Label
        {
            FontSize = 24,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center
        };
        titleHeader.SetBinding(Label.TextProperty, nameof(CourseReportViewModel.ReportTitle));

        var generatedOnLabel = new Label
        {
            FontSize = 14,
            TextColor = Colors.Gray,
            HorizontalOptions = LayoutOptions.Center
        };
        generatedOnLabel.SetBinding(Label.TextProperty, nameof(CourseReportViewModel.GeneratedOn));

        var collectionView = new CollectionView
        {
            SelectionMode = SelectionMode.None,
            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid
                {
                    Padding = 10,
                    ColumnSpacing = 10,
                    RowSpacing = 4,
                    ColumnDefinitions =
                                {
                                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(1.4, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(1.8, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) }
                                }
                };

                var titleLabel = new Label { FontAttributes = FontAttributes.Bold };
                titleLabel.SetBinding(Label.TextProperty, nameof(CourseReportRow.Title));
                grid.Add(titleLabel, 0, 0);

                var statusLabel = new Label();
                statusLabel.SetBinding(Label.TextProperty, nameof(CourseReportRow.Status));
                grid.Add(statusLabel, 1, 0);

                var instructorLabel = new Label();
                instructorLabel.SetBinding(Label.TextProperty, nameof(CourseReportRow.Instructor));
                grid.Add(instructorLabel, 2, 0);

                var startDateLabel = new Label();
                startDateLabel.SetBinding(Label.TextProperty, nameof(CourseReportRow.StartDate));
                grid.Add(startDateLabel, 3, 0);

                var endDateLabel = new Label();
                endDateLabel.SetBinding(Label.TextProperty, nameof(CourseReportRow.EndDate));
                grid.Add(endDateLabel, 4, 0);

                return new Frame
                {
                    BorderColor = Colors.LightGray,
                    CornerRadius = 8,
                    Padding = 0,
                    Content = grid
                };
            })
        };
        collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(CourseReportViewModel.Rows));

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 12,
                Children =
                {
                    titleHeader,
                    generatedOnLabel,
                    collectionView
                }
            }
        };
    }
}