using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Bus.Events
{
    public record OrderCreatedEvent(List<Guid> CouseIds, Guid BuyerId);
}
