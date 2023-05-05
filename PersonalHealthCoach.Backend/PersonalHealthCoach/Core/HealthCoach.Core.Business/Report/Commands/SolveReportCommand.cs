using MediatR;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Business;

public sealed record SolveReportCommand(Guid ReportId, Guid CallerId) : IRequest<Result>;