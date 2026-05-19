using Application.Mappings;
using Application.Mappings.ProductoMap;
using Infraestructure.Mappings;
using Infraestructure.Mappings.ProductMap;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.DI;

public static class MappingsDI
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.ShouldUseConstructor = ci => true;
            cfg.AddProfile<UsuarioProfile>();
            cfg.AddProfile<UsuarioProfileDto>();
            cfg.AddProfile<CategoriaProductoProfile>();
            cfg.AddProfile<CategoriaProductoDtoProfile>();
            cfg.AddProfile<ProductoProfile>();
            cfg.AddProfile<ProductoDtoProfile>();
            cfg.AddProfile<ColoresProfile>();
            cfg.AddProfile<TallasProfile>();
        });
        return services;
    }
}