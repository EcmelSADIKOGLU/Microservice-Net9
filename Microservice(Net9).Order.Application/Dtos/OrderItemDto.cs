
namespace Microservice_Net9_.Order.Application.Dtos
{
    public record OrderItemDto(
        Guid ProductId,
        string ProductName,
        decimal UnitPrice
        );

}
