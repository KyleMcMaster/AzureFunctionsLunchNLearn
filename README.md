# Azure Functions Lunch-N-Learn

Content for Architecture Lunch-n-Learn on Azure Functions

# Getting Started

## Azure Functions CLI

https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=windows%2Cazure-cli


Create an HTTP Trigger Function

```ps
func init EmailFunctions --worker-runtime dotnet-isolated --target-framework net8.0
```

```ps
cd EmailFunctions
```

```ps
func new --name HttpExample --template "HTTP trigger" --authlevel "anonymous"
```

## Durable Orchestrations

https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-isolated-create-first-csharp?pivots=code-editor-vscode