using Microservice_Net9_.Web.DelegateHandlers;
using Microservice_Net9_.Web.ExceptionHandlers;
using Microservice_Net9_.Web.Extensions;
using Microservice_Net9_.Web.Options;
using Microservice_Net9_.Web.Pages.Auth.SignIn;
using Microservice_Net9_.Web.Pages.Auth.SignUp;
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

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<CatalogService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AuthenticatedHttpClientHandler>();
builder.Services.AddScoped<ClientAuthenticatedHttpClientHandler>();

builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();

builder.Services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
{
    var microserviceOption = builder.Configuration.GetRequiredSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();

    configure.BaseAddress = new Uri(microserviceOption!.Catalog.BaseAddress);
})
.AddHttpMessageHandler<AuthenticatedHttpClientHandler>()         //Password
.AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();  //ClientCredential

builder.Services.AddAuthentication(configureOption =>
{
    configureOption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    configureOption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
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

app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
