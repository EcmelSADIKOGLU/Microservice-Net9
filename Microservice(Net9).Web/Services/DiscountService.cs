using Microservice_Net9_.Discount.Api.Features.Discounts;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.Extensions.Logging;

namespace Microservice_Net9_.Web.Services
{
    public class DiscountService(IDiscountRefitService discountRefitService, ILogger<DiscountService> logger)
    {
        public async Task<ServiceResult<DiscountDto>> GetDiscountByCodeAsync(string code)
        {
            var response =  await discountRefitService.GetDiscountByCodeAsync(code);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.Error.Message);
                return ServiceResult<DiscountDto>.Error("An error occurred while fetching discount");
            }
            return ServiceResult<DiscountDto>.Success(response.Content!);
        }
    }
}
