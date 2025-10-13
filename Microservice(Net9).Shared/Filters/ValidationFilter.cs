using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace Microservice_Net9_.Shared.Filters
{
    //Minimal API çalışmadan önce çalışacak olan filter
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
                        
            //Fast Fail (Önce olumsuzu kontrol et)
            if (validator is null)
            {
                return await next(context);
            }

            var requestModel = context.Arguments.OfType<T>().FirstOrDefault();

            if (requestModel is null)
            {
                return await next(context);
            }

            var validatorResult = await validator.ValidateAsync(requestModel);

            if (!validatorResult.IsValid)
            {
                return Results.ValidationProblem(validatorResult.ToDictionary());    
            }


            return await next(context);

        }
    }
}
