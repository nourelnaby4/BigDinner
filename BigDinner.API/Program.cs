using BigDinner.API;
using BigDinner.API.Filters;
using BigDinner.API.Middleware;
using BigDinner.Application;
using BigDinner.Domain;
using BigDinner.Persistence;
using BigDinner.Persistence.Context;
using BigDinner.Service;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApiDependencies(configuration)
    .AddPersistenceDependencies(configuration)
    .AddDatabaseDependencies(configuration)
    .AddApplicationDependencies(configuration)
    .AddDomainDependencies(configuration)
    .AddServiceDependencies(configuration)
    .AddJWtTokenDependencies(configuration);


var CORS = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CORS,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseMiddleware<EnableRequestBodyBufferingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(CORS);
app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
app.Run();


