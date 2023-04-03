using MediatR;
using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public record RetrieveLatestPersonalDataCommand(Guid UserId) : IRequest<Result<PersonalData>>;