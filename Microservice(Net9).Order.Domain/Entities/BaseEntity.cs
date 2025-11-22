
namespace Microservice_Net9_.Order.Domain.Entities
{
    public class BaseEntity<TEntityId>
    {

        public TEntityId Id { get; set; } = default!;

    }
}
