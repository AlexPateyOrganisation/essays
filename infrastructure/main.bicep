param location string = resourceGroup().location

var uniqueId = uniqueString(resourceGroup().id)
var keyVaultName = 'kv-${uniqueId}'

module keyVault 'modules/secrets/key-vault.bicep' = {
name: 'keyVaultDeployment'
  params: {
    vaultName: keyVaultName
    location: location
  }
}

module retrieverApiAppService 'modules/compute/app-service.bicep' = {
  name: 'retrieverApiDeployment'
  params: {
    appServiceName: 'app-retriever-api-${uniqueId}'
    appServicePlanName: 'asp-retriever-api-${uniqueId}'
    location: location
  }
}

module keyVaultRoleAssignment 'modules/secrets/key-vault-role-assignment.bicep' = {
  name: 'keyVaultRoleAssignmentDeployment'
  params: {
    keyVaultName: keyVaultName
    principalIds: [
      retrieverApiAppService.outputs.principalId
    ]
  }
}