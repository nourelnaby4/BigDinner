using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace BigDinner.Domain;

public static class DomainModuleDependencies
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services;
    }
}

