using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;

namespace HealthCoach.Core.Domain;

public class WellnessTip : AggregateRoot
{
    public WellnessTip() { }

    private WellnessTip(string tipText)
    {
        TipText = tipText;
    }

    public static Result<WellnessTip> Create(string tipText)
    {
        var tipResult = tipText.EnsureNotNullOrEmpty(DomainErrors.WellnessTip.Create.TipNullOrEmpty);

        return tipResult.Map(t => new WellnessTip(t));
    }

    public string TipText { get; private set; }
}