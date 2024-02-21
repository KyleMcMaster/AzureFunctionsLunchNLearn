using Clean.Architecture.Core.Contributors;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace NotificationFunctions;

public class NotificationOrchestrator
{
  [Function(nameof(NotificationOrchestrator))]
  public static async Task ExecuteAsync([OrchestrationTrigger] TaskOrchestrationContext context)
  {
    var contributor = context.GetInput<ContributorDTO>()!;
    var logger = context.CreateReplaySafeLogger<NotificationOrchestrator>();

    logger.LogDebug($"Executing Activity {nameof(SayHello)} for contributor {contributor.Name}");
    await context.CallActivityAsync(nameof(SayHello), contributor.Name);

    if (contributor.PhoneNumber is null)
    {
      return;
    }

    var retryPolicy = new RetryPolicy(maxNumberOfAttempts: 2, TimeSpan.FromSeconds(15));
    var retryOptions = new TaskRetryOptions(retryPolicy);
    var options = new TaskOptions(retryOptions);
    await context.CallActivityAsync(nameof(AuditPhoneNumbers), contributor.PhoneNumber, options);
  }

  [Function(nameof(SayHello))]
  public static void SayHello([ActivityTrigger] string contributorName, FunctionContext executionContext)
  {
    string message = $"Hello {contributorName}. 👋 Thanks for your contributions!";
    ILogger logger = executionContext.GetLogger(nameof(SayHello));
    logger.LogInformation(message); // Pretending that this notification is sent to the contributor
  }

  [Function(nameof(AuditPhoneNumbers))]
  public static void AuditPhoneNumbers([ActivityTrigger] string? phoneNumber, FunctionContext executionContext)
  {
    ILogger logger = executionContext.GetLogger(nameof(SayHello));
    logger.LogInformation($"Auditing phone number: {phoneNumber} from Activity {nameof(AuditPhoneNumbers)}");
  }
}
