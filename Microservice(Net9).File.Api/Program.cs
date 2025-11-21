using Microservice_Net9_.File.Api;
using Microservice_Net9_.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//builder.Services.AddVersioningExt();


builder.Services.AddCommonServiceExt(typeof(FileAssembly));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();


