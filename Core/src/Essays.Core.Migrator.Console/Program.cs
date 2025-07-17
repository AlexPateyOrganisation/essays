using Essays.Core.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var connectionString = configuration.GetConnectionString("EssaysContext");

var dbContextOptions = new DbContextOptionsBuilder<EssaysContext>()
    .UseSqlServer(connectionString)
    .Options;

using var dbContext = new EssaysContext(dbContextOptions);

Console.WriteLine("Starting EssaysDb migrations...");
dbContext.Database.Migrate();
Console.WriteLine("Completed EssaysDb migrations...");