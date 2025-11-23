using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Persistence;
using Microservice_Net9_.Order.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.Run();


