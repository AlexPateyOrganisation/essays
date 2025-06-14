param location string = resourceGroup().location
param keyVaultName string

resource keyVault 'Microsoft.keyVault/vaults@2024-12-01-preview' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      name: 'standard'
      family: 'A'
    }
    enableRbacAuthorization: true
    tenantId: subscription().tenantId
  }
}

output id string = keyVault.id
output name string = keyVault.name