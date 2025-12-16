using System.ComponentModel.DataAnnotations;

namespace Microservice_Net9_.Web.Pages.Order.ViewModels;

public class AddressViewModel
{
    [Display(Name = "Address Line")] public string Street { get; set; } = null!;
    [Display(Name = "Address Line")] public string Line { get; set; } = null!;

    [Display(Name = "Province")] public string Province { get; set; } = null!;

    [Display(Name = "District")] public string District { get; set; } = null!;

    [Display(Name = "Zip Code")] public string ZipCode { get; set; } = null!;

    public static AddressViewModel Empty => new();

    public static AddressViewModel Example => new() 
    {
        District = "Kadıköy",
        Line = "No: 23",
        Province = "İstanbul",
        Street = "Bahariye Caddesi",
        ZipCode = "34710"
    };
}
