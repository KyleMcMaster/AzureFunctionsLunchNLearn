using System.Net.Http.Json;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.Contributors;
using Clean.Architecture.Core.Interfaces;

namespace Clean.Architecture.Infrastructure.Notifications;

public class NotificationsService : INotificationService
{
  private readonly HttpClient _httpClient;

  public NotificationsService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task SendSmsNotification(Contributor contributor)
  {
    var dto = new ContributorDTO(contributor.Id, contributor.Name, contributor.PhoneNumber?.Number);
    var result = await _httpClient.PostAsJsonAsync("api/HttpNotificationTrigger", dto);
  }
}
