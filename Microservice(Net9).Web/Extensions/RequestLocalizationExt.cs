using System.Globalization;

namespace Microservice_Net9_.Web.Extensions
{
    public static class RequestLocalizationExt
    {
        public static WebApplication UseRequestLocalizationsExt(this WebApplication app)
        {

            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
                SupportedCultures = [cultureInfo],
                SupportedUICultures = [cultureInfo]
            });

            return app;
        }
    }
}
