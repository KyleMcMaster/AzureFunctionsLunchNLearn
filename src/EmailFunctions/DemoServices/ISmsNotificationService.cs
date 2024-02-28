using Clean.Architecture.Core.Contributors;

namespace NotificationFunctions.DemoServices;

public interface ISmsNotificationService
{
  public Task SendSmsNotification(string phoneNumber, string message);
}
