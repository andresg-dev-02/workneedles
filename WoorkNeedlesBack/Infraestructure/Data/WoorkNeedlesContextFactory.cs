using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Data
{
    public class WoorkNeedlesContextFactory 
        : IDesignTimeDbContextFactory<WoorkNeedlesContext>
    {
        public WoorkNeedlesContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "../WoorkNeedles"
            );

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            var optionsBuilder = new DbContextOptionsBuilder<WoorkNeedlesContext>();

            optionsBuilder.UseNpgsql(connectionString);

            return new WoorkNeedlesContext(optionsBuilder.Options);
        }
    }
}