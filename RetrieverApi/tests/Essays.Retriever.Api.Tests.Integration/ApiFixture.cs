using Essays.Core.Data.Data;
using Essays.Core.Libraries.Extensions;
using Essays.Core.Models.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Essays.Retriever.Api.Tests.Integration;

public class ApiFixture : WebApplicationFactory<IRetrieverApiAssemblyMarker>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer =
        new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        var connectionString = _msSqlContainer.GetConnectionString();
        var dbContextOptions = new DbContextOptionsBuilder<EssaysContext>()
            .UseSqlServer(connectionString).Options;

        await using var dbContext = new EssaysContext(dbContextOptions);
        await dbContext.Database.MigrateAsync();

        await dbContext.Essays
            .AddAsync(new Essay(new Guid("f76a1529-1725-469d-8d27-8fb0f0e61c40"), "Test Title",
                "Test Body".CompressWithGzip(), "Test Author", new DateTime(2025, 7, 1)));
        await dbContext.SaveChangesAsync();

        Environment.SetEnvironmentVariable("ConnectionStrings__EssaysContext", connectionString);
    }

    public new async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await base.DisposeAsync();
    }
}