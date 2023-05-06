using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record CreatePersonalTipCommand(Guid UserId) : IRequest<Result<PersonalTip>>;
   

