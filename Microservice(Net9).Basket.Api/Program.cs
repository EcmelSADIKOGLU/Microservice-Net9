using Microservice_Net9_.Basket.Api;
using Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem;
using Microservice_Net9_.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddVersioningExt();



builder.Services.AddCommonServiceExt(typeof(BasketAssembly));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});


var app = builder.Build();


app.AddBasketGroupEndpointExt(app.AddVersionSetExt());


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();


