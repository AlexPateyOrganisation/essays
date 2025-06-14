param keyVaultName string
param principalIds array

var principalType string = 'ServicePrincipal'
var keyVaultSecretUserRoleDefinitionId string = '4633458b-17de-408a-b874-0445c86b69e6'

resource keyVault 'Microsoft.KeyVault/vaults@2024-12-01-preview' existing = {
  name: keyVaultName
}

resource keyVaultRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = [for principalId in principalIds: {
    name: guid(keyVault.id, principalId, keyVaultSecretUserRoleDefinitionId)
    scope: keyVault
    properties: {
      roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', keyVaultSecretUserRoleDefinitionId)
      principalId: principalId
      principalType: principalType
    } 
  }
]