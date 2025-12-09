namespace Microservice_Net9_.Web.Pages.Instructor.Dtos
{
    public record CreateCourseRequest(
        string Name,
        string Description,
        IFormFile? Picture,
        decimal Price,
        Guid CategoryId);
}
