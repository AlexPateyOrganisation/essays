# Essays

## Running the code locally (with Docker)

### Download [Git](https://git-scm.com/) and run the following commands
```bash
git clone https://github.com/AlexPateyOrganisation/essays.git
cd essays
```

### Download [Docker](https://www.docker.com/) and run the following command

```bash
docker compose up --build
```

## Infrastructure as Code (IaC) Overview

### The following steps describe how to create and deploy the resources defined in the infrastructure folder using Azure as the cloud provider.

### Download [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/)

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