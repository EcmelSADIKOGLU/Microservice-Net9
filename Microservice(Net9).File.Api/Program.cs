using Microservice_Net9_.Bus;
using Microservice_Net9_.File.Api;
using Microservice_Net9_.File.Api.Features.File;
using Microservice_Net9_.Shared.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddVersioningExt();

builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles(); //to access wwwroot files

app.AddFileEndpointGroupExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();


