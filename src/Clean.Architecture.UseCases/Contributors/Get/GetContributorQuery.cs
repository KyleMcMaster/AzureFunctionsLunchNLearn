using Ardalis.Result;
using Ardalis.SharedKernel;
using Clean.Architecture.Core.Contributors;

namespace Clean.Architecture.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
