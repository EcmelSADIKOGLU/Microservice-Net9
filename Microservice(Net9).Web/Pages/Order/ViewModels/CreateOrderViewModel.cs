using Microservice_Net9_.Web.Pages.Basket.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Microservice_Net9_.Web.Pages.Order.ViewModels;

public class CreateOrderViewModel
{
    public AddressViewModel Address { get; set; } = null!;

    public PaymentViewModel Payment { get; set; } = null!;

    [ValidateNever] public List<OrderItemViewModel> OrderItems { get; set; } = [];


    [ValidateNever] public float? DiscountRate { get; set; }


    public decimal TotalPrice { get; set; }

    public static CreateOrderViewModel Empty => new()
    {
        Address = AddressViewModel.Empty,
        Payment = PaymentViewModel.Empty
    };

    public static CreateOrderViewModel Example => new()
    {
        Address = AddressViewModel.Example,
        Payment = PaymentViewModel.Example
    };


    public void AddOrderItem(BasketViewModelItem basketItem)
    {
        OrderItems.Add(new OrderItemViewModel(basketItem.Id, basketItem.Name,
            basketItem.PriceWithDiscountRate ?? basketItem.Price));
    }
}
