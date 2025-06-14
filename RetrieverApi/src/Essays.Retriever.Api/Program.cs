using Essays.Retriever.Api.Endpoints.Internal;
using Essays.Retriever.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureAzureKeyVault();
builder.Services.AddEndpoints<Program>(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("test-key-vault", (IConfiguration configuration) => configuration["TestKey"]);

app.UseEndpoints<Program>();

app.Run();