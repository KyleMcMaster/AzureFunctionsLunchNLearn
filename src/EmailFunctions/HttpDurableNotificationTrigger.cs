using System.Text.Json;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.Contributors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
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

    string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(NotificationOrchestrator.SayHello), contributor);
    _logger.LogInformation("Created new orchestration with instance ID = {instanceId}", instanceId);

    // var status = client.CreateCheckStatusResponse(req, instanceId);

    return new OkObjectResult(new { InstanceId = instanceId });
  }
}
