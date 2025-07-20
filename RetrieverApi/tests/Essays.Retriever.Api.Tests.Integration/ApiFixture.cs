using Essays.Core.Data.Data;
using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Testcontainers.Redis;

namespace Essays.Retriever.Api.Tests.Integration;

public class ApiFixture : WebApplicationFactory<IRetrieverApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer
        = new MsSqlBuilder().Build();

    private readonly RedisContainer _redisContainer
        = new RedisBuilder().Build();

    public async Task InitializeAsync()
    {
        //Set up MSSQL container
        await _msSqlContainer.StartAsync();
        var msSqlConnectionString = _msSqlContainer.GetConnectionString();
        var dbContextOptions = new DbContextOptionsBuilder<EssaysContext>()
            .UseSqlServer(msSqlConnectionString).Options;

        await using var dbContext = new EssaysContext(dbContextOptions);
        await dbContext.Database.MigrateAsync();

        var testEssay = new Essay
        {
            Id = new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"),
            Title = "Test Title",
            CompressedBody =
                "This is a sample essay body for testing purposes. It contains more than one hundred characters to meet the minimum length requirement."
                    .CompressWithGzip(),
            Author = "Test Author",
            CreatedWhen = new DateTime(2025, 7, 1)
        };

        await dbContext.Essays.AddAsync(testEssay);
        await dbContext.SaveChangesAsync();

        Environment.SetEnvironmentVariable("ConnectionStrings__EssaysContext", msSqlConnectionString);

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