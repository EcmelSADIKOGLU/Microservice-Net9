using Microservice_Net9_.Discount.Api.Repositories;

namespace Microservice_Net9_.Discount.Api.Features.Discounts
{
    public class Discount:BaseEntity
    {
        public Guid UserId { get; set; }
        public float Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? ExpireTime { get; set; }

    }
}
