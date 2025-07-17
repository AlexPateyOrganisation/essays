using Essays.Core.Data.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Essays.Writer.Api.Tests.Integration;

public class ApiFixture : WebApplicationFactory<IWriterApiAssemblyMarker>, IAsyncLifetime
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

        Environment.SetEnvironmentVariable("ConnectionStrings__EssaysContext", connectionString);
    }

    public new async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await base.DisposeAsync();
    }
}