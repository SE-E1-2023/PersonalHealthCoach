using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace HealthCoach.Shared.Infrastructure;

public class GenericDbContextFactory : IDesignTimeDbContextFactory<GenericDbContext>
{
    public GenericDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<GenericDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.UseNpgsql(connectionString);

        return new GenericDbContext(builder.Options);
    }
}