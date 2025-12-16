namespace Microservice_Net9_.Web.Pages.Order.Dtos;

public record AddressDto(
    string Province, 
    string District, 
    string Street, 
    string ZipCode, 
    string Line);
