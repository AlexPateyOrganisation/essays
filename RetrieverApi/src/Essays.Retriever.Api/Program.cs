using Essays.Retriever.Api.Endpoints.Internal;
using Essays.Retriever.Api.Extensions;
using Essays.Retriever.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.ConfigureAzureKeyVault();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddEndpoints<Program>(builder.Configuration);
builder.ConfigureOpenTelemetry();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseEndpoints<Program>();

app.Run();