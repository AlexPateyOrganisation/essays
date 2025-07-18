param location string = resourceGroup().location
@secure()
param sqlAdminUser string
@secure()
param sqlAdminPassword string

var uniqueId = uniqueString(resourceGroup().id)
var keyVaultName = 'kv-${uniqueId}'

module keyVault 'modules/secrets/key-vault.bicep' = {
name: 'keyVaultDeployment'
  params: {
    keyVaultName: keyVaultName
    location: location
  }
}

module sqlDatabase 'modules/storage/sql-database.bicep' = {
  name: 'sqlDatabaseDeployment'
  params: {
    sqlServerName: 'sql-essays-${uniqueId}'
    sqlDatabaseName: 'sqldb-essays-${uniqueId}'
    sqlAdminUser: sqlAdminUser
    sqlAdminPassword: sqlAdminPassword
    location: location
    keyVaultName: keyVaultName
  }
}

module retrieverApiAppService 'modules/compute/app-service.bicep' = {
  name: 'retrieverApiDeployment'
  params: {
    appServiceName: 'app-retriever-api-${uniqueId}'
    appServicePlanName: 'asp-retriever-api-${uniqueId}'
    location: location
    keyVaultName: keyVaultName
  }
}

module writerApiAppService 'modules/compute/app-service.bicep' = {
  name: 'writerApiDeployment'
  params: {
    appServiceName: 'app-writer-api-${uniqueId}'
    appServicePlanName: 'asp-writer-api-${uniqueId}'
    location: location
    keyVaultName: keyVaultName
  }
}

module keyVaultRoleAssignment 'modules/secrets/key-vault-role-assignment.bicep' = {
  name: 'keyVaultRoleAssignmentDeployment'
  params: {
    keyVaultName: keyVaultName
    principalIds: [
      retrieverApiAppService.outputs.principalId
      writerApiAppService.outputs.principalId
    ]
  }
}