using Application.Interfaces.User;
using Domain.Ports.Output.UnitOfWork;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Infraestructure.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure.Repositories.UserRepo;

namespace Infraestructure.DI;

public static class PersistenceDI
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WoorkNeedlesContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ConnectionPostgress")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();

        return services;
    }
}