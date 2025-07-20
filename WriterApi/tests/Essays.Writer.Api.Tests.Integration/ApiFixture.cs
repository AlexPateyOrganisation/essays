using Essays.Core.Data.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Testcontainers.Redis;

namespace Essays.Writer.Api.Tests.Integration;

public class ApiFixture : WebApplicationFactory<IWriterApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer =
        new MsSqlBuilder().Build();

    private readonly RedisContainer _redisContainer
        = new RedisBuilder().Build();

    public async Task InitializeAsync()
    {
        //Set up MSSQL container
        await _msSqlContainer.StartAsync();

        var connectionString = _msSqlContainer.GetConnectionString();
        var dbContextOptions = new DbContextOptionsBuilder<EssaysContext>()
            .UseSqlServer(connectionString).Options;

        await using var dbContext = new EssaysContext(dbContextOptions);
        await dbContext.Database.MigrateAsync();

        Environment.SetEnvironmentVariable("ConnectionStrings__EssaysContext", connectionString);

        //Set up Redis container
        await _redisContainer.StartAsync();
        var redisConnectionString = _redisContainer.GetConnectionString();

        Environment.SetEnvironmentVariable("ConnectionStrings__Redis", redisConnectionString);
    }

    public new async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await _redisContainer.DisposeAsync();

        await base.DisposeAsync();
    }
}