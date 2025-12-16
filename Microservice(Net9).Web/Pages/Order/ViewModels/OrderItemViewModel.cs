namespace Microservice_Net9_.Web.Pages.Order.ViewModels;

public record OrderItemViewModel(
Guid ProductId,
string ProductName,
decimal UnitPrice
);
