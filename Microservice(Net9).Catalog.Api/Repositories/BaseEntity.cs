using MongoDB.Bson.Serialization.Attributes;

namespace Microservice_Net9_.Catalog.Api.Repositories
{
    public class BaseEntity
    {
        //snow flake algoritması (index'lemeyi kolaylaştırıyor)
        [BsonElement("_id")] public Guid Id { get; set; }
    }
}
