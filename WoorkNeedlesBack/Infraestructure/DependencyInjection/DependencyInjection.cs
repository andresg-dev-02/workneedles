using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Infraestructure.Mappings;
using Application.Interfaces;


namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WoorkNeedlesContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ConnectionPostgress")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(config => config.AddProfile<UsuarioProfile>());
            services.AddScoped<Application.UseCases.Usuarios.GetAllUsers>();
            return services;
        }
    }
}