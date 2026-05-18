using Infraestructure.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddMappings()
            .AddAuthenticationServices(configuration)
            .AddUseCases();

        return services;
    }
}