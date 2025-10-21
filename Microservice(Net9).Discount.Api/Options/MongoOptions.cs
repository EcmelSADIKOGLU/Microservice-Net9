using System.ComponentModel.DataAnnotations;

namespace Microservice_Net9_.Discount.Api.Options
{
    public class MongoOptions
    {
        [Required] public string DatabaseName { get; set; } = default!;
        [Required] public string ConnectionString { get; set; } = default!;
    }
}
