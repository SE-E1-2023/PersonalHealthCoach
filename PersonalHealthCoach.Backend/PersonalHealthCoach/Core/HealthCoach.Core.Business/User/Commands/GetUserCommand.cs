using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record GetUserCommand(string EmailAddress) : IRequest<Result<Guid>>;