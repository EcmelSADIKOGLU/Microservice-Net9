using System.Text.Json.Serialization;

namespace Microservice_Net9_.Basket.Api.Dto
{
    public record BasketDto ()
    {
        [JsonIgnore]  public Guid UserId { get; init; }
        public List<BasketItemDto> BasketItems { get; init; } = new();
    }


}
