using CSharpFunctionalExtensions;
using MediatR;

namespace HealthCoach.Core.Business;

public sealed record ReportPersonalTipCommand(Guid PersonalTipId, string Reason) : IRequest<Result>;
