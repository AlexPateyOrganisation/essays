param location string = resourceGroup().location

var uniqueId = uniqueString(resourceGroup().id)

module retrievalApiAppService 'modules/compute/app-service.bicep' = {
  name: 'retrievalApiDeployment'
  params: {
    appServiceName: 'app-retrieval-api-${uniqueId}'
    appServicePlanName: 'asp-retrieval-api-${uniqueId}'
    location: location
  }
}