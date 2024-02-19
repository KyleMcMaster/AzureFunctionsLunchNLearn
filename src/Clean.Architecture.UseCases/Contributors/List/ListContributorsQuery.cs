using Ardalis.Result;
using Ardalis.SharedKernel;
using Clean.Architecture.Core.Contributors;

namespace Clean.Architecture.UseCases.Contributors.List;

public record ListContributorsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<ContributorDTO>>>;
