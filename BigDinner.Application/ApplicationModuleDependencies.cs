using BigDinner.Application.Behavior;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace BigDinner.Application;

public static class ApplicationModuleDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));    
        
        services.AddAutoMapper(assembly);

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        services.AddSerilog();

        return services;
    }
}

