# Azure Functions Lunch-N-Learn

Content for Architecture Lunch-n-Learn on Azure Functions

# Getting Started

## Azure Functions CLI

Doesn't build out of the box! Time to try something else.

## VS Code Azure Function Extension

https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp

Still doesn't bind with serialization

https://www.nuget.org/packages/Microsoft.Azure.Functions.Worker.Extensions.Http.AspNetCore/

Closer! But still doesn't work, requires some missing packages not present in the template code. Used this as my example to get it working.

https://github.com/Azure/azure-functions-dotnet-worker/blob/main/samples/FunctionApp/HttpTriggerWithDependencyInjection/HttpTriggerWithDependencyInjection.cs

## Durable Orchestrations

https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-isolated-create-first-csharp?pivots=code-editor-vscode

### Azurite 

https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio-code%2Cblob-storage

```
docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 \
    -v c:/azurite:/data mcr.microsoft.com/azure-storage/azurite
```