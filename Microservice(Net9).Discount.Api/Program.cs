using Microservice_Net9_.Discount.Api;
using Microservice_Net9_.Discount.Api.Features.Discounts;
using Microservice_Net9_.Discount.Api.Options;
using Microservice_Net9_.Discount.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddVersioningExt();

builder.Services.AddOptionsExt();
builder.Services.AddRepositoryExt();

builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));

var app = builder.Build();

app.AddDiscountEndpointGroupExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();


