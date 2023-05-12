using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record CreateWellnessPlanCommand(Guid UserId) : IRequest<Result<CreateWellnessPlanCommandResponse>>;

public sealed record CreateWellnessPlanCommandResponse(string Title, string Description, IReadOnlyCollection<string> Categories);