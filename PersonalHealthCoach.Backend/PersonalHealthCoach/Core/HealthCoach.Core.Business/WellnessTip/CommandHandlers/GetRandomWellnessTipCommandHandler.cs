using CSharpFunctionalExtensions;
using HealthCoach.Core.Domain;
using HealthCoach.Shared.Core;
using HealthCoach.Shared.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;


using Errors = HealthCoach.Core.Business.BusinessErrors.WellnessTip.GetRandomTip;

namespace HealthCoach.Core.Business;

public class GetRandomWellnessTipCommandHandler : IRequestHandler<GetRandomWellnessTipCommand, Result<WellnessTip>>
{
    private readonly IEfQueryProvider queryProvider;

    public GetRandomWellnessTipCommandHandler(IEfQueryProvider queryProvider)
    {
        this.queryProvider = queryProvider;
    }
    public async Task<Result<WellnessTip>> Handle(GetRandomWellnessTipCommand request, CancellationToken cancellationToken)
    {
        var tips = queryProvider.Query<WellnessTip>();
        int rowsNumber = tips.Count();
        int randomNumber = new System.Random().Next(rowsNumber);

        return tips
            .Skip(randomNumber)
            .Take(1)
            .First()
            .EnsureNotNull(Errors.TipDoesNotExist)!;
    }
}
