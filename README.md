# Essays

## Infrastructure as Code (IaC) Overview

### The cloud provider chosen is Azure. The following steps detail how to create and deploy the required cloud resources to a given subcription group in a given directory.

### Download Azure CLI
https://learn.microsoft.com/en-us/cli/azure/

### Log in to your Azure account
```bash
az login
```

### Create a Resource Group (e.g., for development environment resources)
```bash
az group create --name rg-essays-dev --location uksouth
```

### Test infrastructure deployment before creating
```bash
az deployment group what-if --resource-group rg-essays-dev --template-file infrastructure/main.bicep
```

### Deploy infrastructure
```bash
az deployment group create --resource-group rg-essays-dev --template-file infrastructure/main.bicep
```

### Get Publish Profile for a given Azure Web App
```bash
az webapp deployment list-publishing-profiles --name app-retrieval-api-rv4qakzmk4hiy --resource-group rg-essays-dev --xml
```