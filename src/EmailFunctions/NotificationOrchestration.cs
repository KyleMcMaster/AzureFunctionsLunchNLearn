using Clean.Architecture.Core.Contributors;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace NimblePros.NotificationFunctions;

public class NotificationOrchestration
{
  private static readonly TaskOptions _retryPolicy = new(new TaskRetryOptions(new RetryPolicy(maxNumberOfAttempts: 2, TimeSpan.FromSeconds(15))));

  [Function(nameof(NotificationOrchestration))]
  public static async Task RunAsync([OrchestrationTrigger] TaskOrchestrationContext context)
  {
    var contributor = context.GetInput<ContributorDTO>()!;
    var logger = context.CreateReplaySafeLogger<NotificationOrchestration>();

    logger.LogDebug($"Executing Activity {nameof(SayHello)} for contributor {contributor.Name}");
    await context.CallActivityAsync(nameof(SayHello), contributor.Name);

    if (contributor.PhoneNumber is null)
    {
      // await context.CallActivityAsync(nameof(EmailInsteadOfTextNotification), contributor.Id);
      return;
    }

    await context.CallActivityAsync(nameof(AuditPhoneNumberForCompliance), contributor.PhoneNumber, _retryPolicy);
  }

  [Function(nameof(SayHello))]
  public static void SayHello([ActivityTrigger] string contributorName, FunctionContext executionContext)
  {
    string message = $"Hello {contributorName}. 👋 Thanks for your contributions!";
    var logger = executionContext.GetLogger(nameof(SayHello));
    logger.LogInformation(message); // Pretending that this notification is sent to the contributor
  }
}
