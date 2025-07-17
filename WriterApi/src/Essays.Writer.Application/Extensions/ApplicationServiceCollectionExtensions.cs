using Essays.Core.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Essays.Writer.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:EssaysContext"] ??
                               throw new Exception("Could not find connection string for Essays Db.");

        services.AddDbContext<EssaysContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(connectionString),
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton);
    }
}