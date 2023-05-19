using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record GetAllPersonalDataCommand(Guid UserId) : IRequest<Result<List<PersonalData>>>;