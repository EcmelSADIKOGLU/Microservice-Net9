using Microservice_Net9_.Web.DelegateHandlers;
using Microservice_Net9_.Web.Extensions;
using Microservice_Net9_.Web.Pages.Auth.SignIn;
using Microservice_Net9_.Web.Pages.Auth.SignUp;
using Microservice_Net9_.Web.Pages.Options;
using Microservice_Net9_.Web.Services;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddOptionsExt();

builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddHttpClient<TokenService>();

builder.Services.AddScoped<CatalogService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
{
    var gatewayOption = builder.Configuration.GetRequiredSection(nameof(GatewayOption)).Get<GatewayOption>();

    configure.BaseAddress = new Uri(gatewayOption!.BaseAddress);
})
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>()         //Password
.AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();  //ClientCredential

builder.Services.AddAuthentication(configureOptions =>
    {
        configureOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        configureOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.Cookie.Name = "Microservice.Web.Cookie";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
