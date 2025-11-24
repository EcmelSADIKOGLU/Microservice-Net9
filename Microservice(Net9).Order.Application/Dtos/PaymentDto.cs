using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Order.Application.Dtos
{
    public record PaymentDto(
        string CardNumber,
        string CardHolderName,
        DateTime ExpirationDate,
        string Cvv,
        decimal Amount
        );

}
