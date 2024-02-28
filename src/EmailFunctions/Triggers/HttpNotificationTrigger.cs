using System.Text.Json;
using Clean.Architecture.Core.Contributors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NotificationFunctions.DemoServices;

namespace NotificationFunctions.Triggers;

public class HttpNotificationTrigger(ILogger<HttpNotificationTrigger> logger, ISmsNotificationService notificationService)
{
  private readonly ILogger<HttpNotificationTrigger> _logger = logger;

  [Function(nameof(HttpNotificationTrigger))]
  public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    var body = await new StreamReader(req.Body).ReadToEndAsync();
    var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    var contributor = JsonSerializer.Deserialize<ContributorDTO>(body, options);

    if (contributor is null)
    {
      return new BadRequestResult();
    }

    var message = $"Hello {contributor.Name}. 👋 Thanks for your contributions!";
    await notificationService.SendSmsNotification(contributor.PhoneNumber!, message);

    return new OkObjectResult(new { Message = message });
  }
}
