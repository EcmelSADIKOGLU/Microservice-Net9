namespace Microservice_Net9_.Web.Pages.Instructor.ViewModels;
public record CourseViewModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    string CreateTime,
    string EducatorFullName,
    string CategoryName,
    int Duration,
    float Rating)
{
    public static CourseViewModel Empty => new CourseViewModel(
        Guid.Empty,
        string.Empty,
        string.Empty,
        0,
        null,
        string.Empty,
        string.Empty,
        string.Empty,
        0,
        0
        );

    public string TruncateDescription(int maxLength)
    {
        if (Description.Length <= maxLength) return Description;
        return Description.Substring(0, maxLength) + "...";
    }
}

