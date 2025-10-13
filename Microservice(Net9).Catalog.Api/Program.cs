using MediatR;
using Microservice_Net9_.Catalog.Api.Features.Categories;
using Microservice_Net9_.Catalog.Api.Features.Categories.Create;
using Microservice_Net9_.Catalog.Api.Options;
using Microservice_Net9_.Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOptionsExt();
builder.Services.AddRepositoryExt();



var app = builder.Build();

app.AddCategoryGroupEndpointExt();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.Run();

