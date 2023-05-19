using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record CreatePersonalTipCommand(Guid UserId) : IRequest<Result<PersonalTip>>;