using Microservice_Net9_.Order.Api;
using Microservice_Net9_.Order.Api.Endpoints.Orders;
using Microservice_Net9_.Order.Application;
using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Application.Contracts.UnitOfWork;
using Microservice_Net9_.Order.Persistence;
using Microservice_Net9_.Order.Persistence.Repositories;
using Microservice_Net9_.Order.Persistence.UnitOfWork;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs. To swaagger see endpoints
builder.Services.AddSwaggerGen();

builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));

builder.Services.AddVersioningExt();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}



app.Run();


