using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure.Data;
using Domain.Interfaces;
using Infraestructure.Repositories;


namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WoorkNeedlesContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ConnectionPostgress")));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}