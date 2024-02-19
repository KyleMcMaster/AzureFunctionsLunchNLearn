using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NimblePros.NotificationFunctions;

public class HttpNotificationTrigger(ILogger<HttpNotificationTrigger> logger)
{
  private readonly ILogger<HttpNotificationTrigger> _logger = logger;

  [Function(nameof(HttpNotificationTrigger))]
  public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    return new OkObjectResult("Welcome to Azure Functions!");
  }
}
