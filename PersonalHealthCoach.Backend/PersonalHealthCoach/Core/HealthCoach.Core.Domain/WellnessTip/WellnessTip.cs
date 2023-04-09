using CSharpFunctionalExtensions;
using HealthCoach.Shared.Core;


namespace HealthCoach.Core.Domain;

public class WellnessTip : AggregateRoot
{
    public WellnessTip() { }

    public WellnessTip(string tipText)
    {
        TipText = tipText;
    }

    public static Result<WellnessTip> Create(string tipText)
    {
        var tipResult = tipText.EnsureNotNullOrEmpty(DomainErrors.WellnessTip.Create.TipNullOrEmpty);

        return Result.FirstFailureOrSuccess(tipResult).Map(() => new WellnessTip(tipText));
    }

    public string TipText { get; private set; }
}


