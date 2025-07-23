param location string = resourceGroup().location
param appServicePlanName string
param appServiceName string
param keyVaultName string
param logAnalyticsWorkspaceId string

module applicationInsights '../telemetry/app-insights.bicep' = {
    name: '${appServiceName}ApplicationInsightsDeployment'
    params: {
        location: location
        name: 'appi-${appServiceName}'
        logAnalyticsWorkspaceId: logAnalyticsWorkspaceId
    }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = {
  kind: 'app,linux'
  location: location
  name: appServicePlanName
  properties: {
    reserved: true
  }
  sku: {
    name: 'B1'
  }
}

resource webApp 'Microsoft.Web/sites@2024-04-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|9.0'
      appSettings: [
        {
          name: 'KeyVaultName'
          value: keyVaultName
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: applicationInsights.outputs.connectionString
        }
      ]
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

output principalId string = webApp.identity.principalId