param name string

resource staticWebApp 'Microsoft.Web/staticSites@2024-11-01' = {
  location: 'westeurope'
  name: name
  sku: {
    tier: 'Standard'
    name: 'Standard'
  }
}

output id string = staticWebApp.id
output url string = 'https://${staticWebApp.properties.defaultHostname}'