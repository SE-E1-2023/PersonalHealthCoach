using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using MediatR;

namespace HealthCoach.Core.Business;

public record RetrieveLatestPersonalDataCommand(Guid UserId): IRequest<Result<PersonalData>>;