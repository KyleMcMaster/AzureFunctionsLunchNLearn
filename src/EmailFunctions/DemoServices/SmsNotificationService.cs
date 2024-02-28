namespace NotificationFunctions.DemoServices;

public class SmsNotificationService : ISmsNotificationService
{
  public Task SendSmsNotification(string phoneNumber, string message)
  {
    return Task.CompletedTask;
  }
}
