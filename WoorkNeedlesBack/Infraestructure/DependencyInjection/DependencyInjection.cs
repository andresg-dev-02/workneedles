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
using Application.Mappings;
using Domain.Ports.Output;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infraestructure.Services.SecurityParameters;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WoorkNeedlesContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ConnectionPostgress")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(cfg =>
            {
                cfg.ShouldUseConstructor = ci => true;
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<UsuarioProfileDto>();
            });
            services.AddScoped<Application.UseCases.Users.GetAllUsers>();
            services.AddScoped<Application.UseCases.Users.GetUserById>();
            services.AddScoped<Application.UseCases.Users.DeleteUser>();
            services.AddScoped<Application.UseCases.Login.Auth>();
            services.AddScoped<IPasswordHash, PasswordHash>();
            services.AddScoped<Application.UseCases.Users.AddUser>();
            services.AddScoped<Application.UseCases.Users.UpdateUser>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                });
            return services;
        }
    }
}