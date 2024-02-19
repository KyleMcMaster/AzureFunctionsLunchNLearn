using Ardalis.Result;
using Ardalis.SharedKernel;
using Clean.Architecture.Core.Contributors;

namespace Clean.Architecture.UseCases.Contributors.Update;

public record UpdateContributorCommand(int ContributorId, string NewName) : ICommand<Result<ContributorDTO>>;
