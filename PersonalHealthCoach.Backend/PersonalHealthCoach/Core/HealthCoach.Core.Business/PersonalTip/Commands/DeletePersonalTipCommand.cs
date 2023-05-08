using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record DeletePersonalTipCommand(Guid PersonalTipId) : IRequest<Result>;