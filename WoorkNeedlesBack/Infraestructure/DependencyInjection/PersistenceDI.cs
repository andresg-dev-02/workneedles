using Application.Interfaces.User;
using Domain.Ports.Output.UnitOfWork;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Infraestructure.Repositories.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure.Repositories.UserRepo;
using Domain.Ports.Output.Email;
using Infraestructure.Repositories.Email;

namespace Infraestructure.DI;

public static class PersistenceDI
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WoorkNeedlesContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
        services.AddScoped<IEmailService, EmailRepository>();
        

        return services;
    }
}