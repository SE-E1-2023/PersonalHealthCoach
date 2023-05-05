using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record ReportDietPlanCommand(Guid DietPlanId, string Reason) : IRequest<Result>;
