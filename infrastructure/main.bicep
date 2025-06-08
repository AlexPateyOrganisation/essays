param location string = resourceGroup().location

var uniqueId = uniqueString(resourceGroup().id)

module retrieverApiAppService 'modules/compute/app-service.bicep' = {
  name: 'retrieverApiDeployment'
  params: {
    appServiceName: 'app-retriever-api-${uniqueId}'
    appServicePlanName: 'asp-retriever-api-${uniqueId}'
    location: location
  }
}