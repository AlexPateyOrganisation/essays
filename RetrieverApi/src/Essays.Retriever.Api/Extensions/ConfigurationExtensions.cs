using System.Reflection;
using Azure.Identity;
using Azure.Monitor.OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Essays.Retriever.Api.Extensions;

public static class ConfigurationExtensions
{
    public static void ConfigureAzureKeyVault(this IConfigurationManager configuration)
    {
        var keyVaultName = configuration["KeyVaultName"];
        if (!string.IsNullOrWhiteSpace(keyVaultName))
        {
            configuration.AddAzureKeyVault(
                new Uri($"https://{keyVaultName}.vault.azure.net/"),
                new DefaultAzureCredential());
        }
    }

    public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        const string serviceName = "essays-retriever-api";
        const string serviceNamespace = "Essays.Retriever.Api";

        var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
        var applicationInsightsConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName, serviceNamespace, serviceVersion: assemblyVersion)
                .AddAttributes([new KeyValuePair<string, object>("environment", builder.Environment.EnvironmentName)]))
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddEntityFrameworkCoreInstrumentation(options =>
                        options.SetDbStatementForText = builder.Environment.IsDevelopment());
                tracing.AddRedisInstrumentation();
                tracing.AddConsoleExporter();
                if (applicationInsightsConnectionString is null)
                {
                    var otlpEndpoint = builder.Configuration["OpenTelemetry:OTLP_Endpoint"] ??
                                       throw new Exception("Could not find url for OpenTelemetry Collector.");

                    tracing.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint));
                }
                else
                {
                    tracing.AddAzureMonitorTraceExporter(options
                        => options.ConnectionString = applicationInsightsConnectionString);
                }
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddMeter("Microsoft.AspNetCore.Hosting");
                metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
                metrics.AddConsoleExporter();
                if (applicationInsightsConnectionString is null)
                {
                    var otlpEndpoint = builder.Configuration["OpenTelemetry:OTLP_Endpoint"] ??
                                       throw new Exception("Could not find url for OpenTelemetry Collector.");

                    metrics.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint));
                }
                else
                {
                    metrics.AddAzureMonitorMetricExporter(options =>
                        options.ConnectionString = applicationInsightsConnectionString);
                }
            })
            .WithLogging(logging =>
            {
                logging.AddConsoleExporter();
                if (applicationInsightsConnectionString is null)
                {
                    var otlpEndpoint = builder.Configuration["OpenTelemetry:OTLP_Endpoint"] ??
                                       throw new Exception("Could not find url for OpenTelemetry Collector.");

                    logging.AddOtlpExporter(options => options.Endpoint = new Uri(otlpEndpoint));
                }
                else
                {
                    logging.AddAzureMonitorLogExporter(options =>
                        options.ConnectionString = applicationInsightsConnectionString);
                }
            });
    }
}