using BigDinner.API.Behavior;
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
        services.AddCarter();
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));   //validation behavior 
        Log.Logger=new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        services.AddSerilog();
        return services;
    }
}

