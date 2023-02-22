Execute this command to create infrastructure:
``` bash
az login
az deployment group create --name netcoreconforleanstest --resource-group NetCoreConf2023-MSOrleansV3 --template-file main.bicep
```
Before you need to add a base name for resources like: "jmfzorleanstest"