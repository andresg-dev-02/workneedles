using Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.DI;

public static class UseCasesDI
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<GetAllUsers>()
            .AddClasses(classes => classes.InNamespaces(
                "Application.UseCases.Users",
                "Application.UseCases.Login",
                "Application.UseCases.Products.Categorias",
                "Application.UseCases.Products.Producto"
            ))
            .AsSelf()
            .WithScopedLifetime());

        return services;
    }
}