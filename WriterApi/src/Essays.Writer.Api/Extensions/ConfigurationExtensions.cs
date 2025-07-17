using Azure.Identity;

namespace Essays.Writer.Api.Extensions;

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
}