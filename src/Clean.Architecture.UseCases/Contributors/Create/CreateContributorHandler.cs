using Ardalis.Result;
using Ardalis.SharedKernel;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.Interfaces;

namespace Clean.Architecture.UseCases.Contributors.Create;

public class CreateContributorHandler(IRepository<Contributor> _repository, INotificationService _notificationService)
  : ICommandHandler<CreateContributorCommand, Result<int>>
{
  public async Task<Result<int>> Handle(CreateContributorCommand request,
    CancellationToken cancellationToken)
  {
    var newContributor = new Contributor(request.Name);
    if (!string.IsNullOrEmpty(request.PhoneNumber))
    {
      newContributor.SetPhoneNumber(request.PhoneNumber);
    }
    var createdItem = await _repository.AddAsync(newContributor, cancellationToken);

    await _notificationService.SendSmsNotification(newContributor);

    return createdItem.Id;
  }
}
