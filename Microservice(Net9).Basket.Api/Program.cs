using Microservice_Net9_.Basket.Api;
using Microservice_Net9_.Basket.Api.Features.Basket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<BasketService>();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();


app.AddBasketGroupEndpointExt(app.AddVersionSetExt());


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();


