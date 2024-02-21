using System.Text.Json;
using Clean.Architecture.Core.Contributors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using NotificationFunctions;

namespace NimblePros.NotificationFunctions;

public class HttpDurableNotificationTrigger(ILogger<HttpDurableNotificationTrigger> logger)
{
  private readonly ILogger<HttpDurableNotificationTrigger> _logger = logger;

  [Function(nameof(HttpDurableNotificationTrigger))]
  public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
    [DurableClient] DurableTaskClient client,
    FunctionContext executionContext)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    string body = await new StreamReader(req.Body).ReadToEndAsync();
    var contributor = JsonSerializer.Deserialize<ContributorDTO>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web));

    if (contributor is null)
    {
      return new BadRequestResult();
    }

    string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(NotificationOrchestration), contributor);
    _logger.LogInformation("Created new orchestration with instance ID = {instanceId}", instanceId);

    return new OkObjectResult(new { InstanceId = instanceId });
  }
}

/*
{
    "name":"ExecuteAs",
    "instanceId":"7f99f9474a6641438e5c7169b7ecb3f2",
    "runtimeStatus":"Completed",
    "input":null,
    "customStatus":null,
    "output":"Hello, Tokyo! Hello, London! Hello, Seattle!",
    "createdTime":"2023-01-31T18:48:49Z",
    "lastUpdatedTime":"2023-01-31T18:48:56Z"
}
*/
