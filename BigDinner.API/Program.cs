using BigDinner.API;
using BigDinner.Application;
using BigDinner.Domain;
using BigDinner.Persistence;
using BigDinner.Service;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;
    builder.Services.AddApiDependencies(configuration)
        .AddApplicationDependencies(configuration)
        .AddDomainDependencies(configuration)
        .AddPersistenceDependencies(configuration)
        .AddServiceDependencies(configuration)
        .AddJWtTokenDependencies(configuration);
}
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.Run();
}

