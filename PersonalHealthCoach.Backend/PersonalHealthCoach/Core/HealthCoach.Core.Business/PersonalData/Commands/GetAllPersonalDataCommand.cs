using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record GetAllPersonalDataCommand(Guid UserId) : IRequest<Result<List<PersonalData>>>;