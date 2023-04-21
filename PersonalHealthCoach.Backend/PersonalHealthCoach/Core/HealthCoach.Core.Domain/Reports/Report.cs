using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;

namespace HealthCoach.Core.Domain;

public sealed class Report : AggregateRoot
{
    public Report() { }

    private Report(Guid targetId, string target, string reason)
    {
        TargetId = targetId;
        Target = target;
        Reason = reason;
        ReportedAt = TimeProvider.Instance().UtcNow;
    }

    public static Result<Report> Create(Guid targetId, string target, string reason)
    {
        var targetResult = target
            .EnsureNotNullOrEmpty(DomainErrors.Report.Create.TargetNullOrEmpty)
            .Ensure(t => ReportConstants.AllowedTargets.Contains(t), DomainErrors.Report.Create.InvalidTarget);
        var reasonResult = reason.EnsureNotNullOrEmpty(DomainErrors.Report.Create.ReasonNullOrEmpty);

        return Result.FirstFailureOrSuccess(targetResult, reasonResult)
            .Map(() => new Report(targetId, target, reason));
    }

    public Guid TargetId { get; private set; }

    public string Target { get; private set; }

    public string Reason { get; private set; }

    public DateTime ReportedAt { get; private set; }

    public DateTime? SolvedAt { get; private set; }
}