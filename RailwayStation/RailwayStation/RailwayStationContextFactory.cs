using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayStation
{
    internal class RailwayStationContextFactory : IDesignTimeDbContextFactory<RailwayStationContext>
    {
        public RailwayStationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RailwayStationContext>();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string? connectionString = config.GetConnectionString("MyConnection");
            optionsBuilder.UseSqlServer(connectionString!);
            return new RailwayStationContext(optionsBuilder.Options);
        }
    }
}
