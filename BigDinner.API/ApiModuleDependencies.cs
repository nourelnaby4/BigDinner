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
        return services;
    }
}

