using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace NotificationFunctions.NotificationWorkflow;

[DurableTask(nameof(AuditPhoneNumberForCompliance))]
public partial class AuditPhoneNumberForCompliance : TaskActivity<string, string>
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

//public partial class AuditPhoneNumberForCompliance
//{
//  [Function(nameof(AuditPhoneNumberForComplianceMethod))]
//  public void AuditPhoneNumberForComplianceMethod([ActivityTrigger] string phoneNumber, FunctionContext executionContext)
//  {
//    var logger = executionContext.GetLogger<AuditPhoneNumberForCompliance>();
//    logger.LogInformation($"Auditing phone number: {phoneNumber} from Activity {nameof(AuditPhoneNumberForCompliance)}");
//  }
//}
