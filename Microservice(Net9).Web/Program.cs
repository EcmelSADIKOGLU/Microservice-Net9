using Microservice_Net9_.Web.ExceptionHandlers;
using Microservice_Net9_.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();


builder.Services.AddOptionsExt();
builder.Services.AddScopeExt();
builder.Services.AddRefitServicesExt(builder.Configuration);
builder.Services.AddAuthenticationConfigurationExt();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRequestLocalizationsExt();

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
