using Essays.Retriever.Api.Endpoints.Internal;
using Essays.Retriever.Api.Extensions;
using Essays.Retriever.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? 
    throw new Exception("Could not find allowed origins value from configuration.");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Configuration.ConfigureAzureKeyVault();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddEndpoints<Program>(builder.Configuration);
builder.ConfigureOpenTelemetry();

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.UseEndpoints<Program>();

app.Run();