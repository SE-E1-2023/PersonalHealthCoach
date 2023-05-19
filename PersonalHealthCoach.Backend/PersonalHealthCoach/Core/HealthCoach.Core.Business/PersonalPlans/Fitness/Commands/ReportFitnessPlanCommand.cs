using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record ReportFitnessPlanCommand(Guid FitnessPlanId, string Reason) : IRequest<Result>;
