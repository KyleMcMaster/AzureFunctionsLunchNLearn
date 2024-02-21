using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.Contributors;

namespace Clean.Architecture.Core.Interfaces;

public interface INotificationService
{
  public Task SendSmsNotification(Contributor contributor);
}
