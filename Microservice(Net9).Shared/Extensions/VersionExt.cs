using Microsoft.Extensions.DependencyInjection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;

namespace Microservice_Net9_.Shared.Extensions
{
    public static class VersionExt
    {
        public static IServiceCollection AddVersioningExt(this IServiceCollection services)
        {


            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1,0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true; //response hadderda gösterir
                options.ApiVersionReader = new UrlSegmentApiVersionReader(); //versiyon bilgisi urlden okunur


            }).AddApiExplorer(options =>
            {
                //Swagger için
                options.GroupNameFormat = "'v'V"; //v1, v2
                options.SubstituteApiVersionInUrl = true; //urlde versiyon bilgisini değiştirir
            });

            return services;
        }

        //MinimalAPI için
        public static ApiVersionSet AddVersionSetExt(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .HasApiVersion(new ApiVersion(2, 0))
                .ReportApiVersions()
                .Build();

            return apiVersionSet;
        }
    }
}
