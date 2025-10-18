using Microservice_Net9_.Catalog.Api;
using Microservice_Net9_.Catalog.Api.Features.Categories;
using Microservice_Net9_.Catalog.Api.Features.Courses;
using Microservice_Net9_.Catalog.Api.Options;
using Microservice_Net9_.Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOptionsExt();
builder.Services.AddRepositoryExt();
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));

var app = builder.Build();

app.AddCategoryGroupEndpointExt();
app.AddCourseGroupEndpointExt();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}



app.Run();

