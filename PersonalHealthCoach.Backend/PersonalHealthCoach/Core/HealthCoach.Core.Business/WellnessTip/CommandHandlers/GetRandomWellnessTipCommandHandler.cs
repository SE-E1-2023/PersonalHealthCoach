using MediatR;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using CSharpFunctionalExtensions;
using HealthCoach.Shared.Infrastructure;

using Errors = HealthCoach.Core.Business.BusinessErrors.WellnessTip.GetRandomTip;

namespace HealthCoach.Core.Business;

internal sealed class GetRandomWellnessTipCommandHandler : IRequestHandler<GetRandomWellnessTipCommand, Result<WellnessTip>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetRandomWellnessTipCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }
    
    public async Task<Result<WellnessTip>> Handle(GetRandomWellnessTipCommand request, CancellationToken cancellationToken)
    {
        var tips = queryProvider.Query<WellnessTip>();
        var rowsNumber = tips.Count();
        var randomNumber = new Random().Next(rowsNumber);

        return tips
            .Skip(randomNumber)
            .Take(1)
            .First()
            .EnsureNotNull(Errors.TipDoesNotExist)!;
    }
}
