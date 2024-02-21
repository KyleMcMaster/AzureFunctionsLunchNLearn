using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace NimblePros.NotificationFunctions;

[DurableTask(nameof(AuditPhoneNumberForCompliance))]
public class AuditPhoneNumberForCompliance : TaskActivity<string, string>
{
  private readonly ILogger<AuditPhoneNumberForCompliance> _logger;

  public AuditPhoneNumberForCompliance(ILogger<AuditPhoneNumberForCompliance> logger)
  {
    _logger = logger;
  }

  public override Task<string> RunAsync(TaskActivityContext context, string input)
  {
    _logger.LogInformation($"Auditing phone number: {input} from Activity {nameof(AuditPhoneNumberForCompliance)}");

    return Task.FromResult(input);
  }
}
