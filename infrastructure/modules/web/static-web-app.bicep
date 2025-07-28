param name string

resource staticWebApp 'Microsoft.Web/staticSites@2024-11-01' = {
  kind: 'app,linux'
  location: 'westeurope'
  name: name
  sku: {
    tier: 'Standard'
    name: 'Standard'
  }
  properties: {
    
  }
}

output id string = staticWebApp.id
output url string = 'https://${staticWebApp.properties.defaultHostname}'