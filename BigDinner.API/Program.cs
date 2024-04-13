using BigDinner.API;
using BigDinner.API.Middleware;
using BigDinner.Application;
using BigDinner.Domain;
using BigDinner.Persistence;
using BigDinner.Service;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApiDependencies(configuration)
    .AddApplicationDependencies(configuration)
    .AddDomainDependencies(configuration)
    .AddPersistenceDependencies(configuration)
    .AddDatabaseDependencies(configuration)
    .AddServiceDependencies(configuration)
    .AddJWtTokenDependencies(configuration);
builder.Services.AddCarter();


var app = builder.Build();

app.MapCarter();
app.UseMiddleware<GlobalExceptionHandlerMiddelware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();


