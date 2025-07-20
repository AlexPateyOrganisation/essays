using Essays.Core.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Essays.Core.Migrator.Console;

public class EssaysContextDesignTimeFactory : IDesignTimeDbContextFactory<EssaysContext>
{
    public EssaysContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("EssaysContext");

        var dbContextOptions = new DbContextOptionsBuilder<EssaysContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new EssaysContext(dbContextOptions);
    }
}