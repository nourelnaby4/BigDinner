using BigDinner.API.Filters;
using FluentValidation;
using Serilog;
using System.Reflection;

namespace BigDinner.API;

public static class ApiModuleDependencies
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilterAttribute>();
        });
        return services;
    }
}

