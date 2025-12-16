using Microservice_Net9_.Discount.Api.Features.Discounts;
using Refit;

namespace Microservice_Net9_.Web.Services.Refit
{
    public interface IDiscountRefitService
    {
        [Get("/api/v1/discounts/{code}")]
        Task<ApiResponse<DiscountDto>> GetDiscountByCodeAsync(string code);
    }
}
