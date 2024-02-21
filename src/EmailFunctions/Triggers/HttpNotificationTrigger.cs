using System.Text.Json;
using Clean.Architecture.Core.Contributors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace NotificationFunctions.Triggers;

public class HttpNotificationTrigger(ILogger<HttpNotificationTrigger> logger)
{
  private readonly ILogger<HttpNotificationTrigger> _logger = logger;

  [Function(nameof(HttpNotificationTrigger))]
  public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var contributor = JsonSerializer.Deserialize<ContributorDTO>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web));

    if (contributor is null)
    {
      return new BadRequestResult();
    }

    var message = $"Hello {contributor.Name}. 👋 Thanks for your contributions!";
    _logger.LogInformation(message); // Pretending that this notification is sent to the contributor

    return new OkObjectResult(new { Message = message });
  }
}
