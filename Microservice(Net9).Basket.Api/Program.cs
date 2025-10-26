using Microservice_Net9_.Basket.Api;
using Microservice_Net9_.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();






builder.Services.AddCommonServiceExt(typeof(BasketAssembly));



var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();


