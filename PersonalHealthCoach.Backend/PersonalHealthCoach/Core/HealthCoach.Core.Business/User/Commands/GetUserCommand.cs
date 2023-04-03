using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record GetUserCommand(string EmailAddress) : IRequest<Result<Guid>>;