param location string = resourceGroup().location
param appServicePlanName string
param appServiceName string
param keyVaultName string

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
      ]
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

output principalId string = webApp.identity.principalId