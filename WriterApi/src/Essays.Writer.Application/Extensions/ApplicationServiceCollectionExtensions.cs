using Essays.Core.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

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

    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration["ConnectionStrings:Redis"] ??
            throw new Exception("Could not find connection string for Redis.");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
    }
}