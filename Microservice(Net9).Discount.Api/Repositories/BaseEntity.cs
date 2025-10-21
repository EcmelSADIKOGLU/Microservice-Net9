using MongoDB.Bson.Serialization.Attributes;

namespace Microservice_Net9_.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
