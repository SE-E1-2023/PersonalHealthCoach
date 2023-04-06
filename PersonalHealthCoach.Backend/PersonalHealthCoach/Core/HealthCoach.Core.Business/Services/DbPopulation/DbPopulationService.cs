using HealthCoach.Core.Domain;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using HealthCoach.Shared.Infrastructure;

namespace HealthCoach.Core.Business;

public class DbPopulationService
{
    private readonly IRepository repository;
    private readonly IEfQueryProvider queryProvider;

    public DbPopulationService(IRepository repository, IEfQueryProvider queryProvider)
    {
        this.repository = repository;
        this.queryProvider = queryProvider;
    }

    public async Task PopulateDb()
    {
        await PopulateUsers();
    }

    private async Task PopulateUsers()
    {
        var users = new List<Result<User>>
        {
            User.Create("Tescu", "Tudor", "tudor.tescu@hotmail.com"),
            User.Create("Pintea", "Fabian", "pintea2002@gmail.com")
        };

        var dbUsers = await queryProvider.Query<User>().ToListAsync();

        foreach (var user in users)
        {
            if (user.IsSuccess && dbUsers.Any(u => u.EmailAddress == user.Value.EmailAddress))
            {
                continue;
            }
            
            await user.Tap(u => repository.Store(u));
        }
    }

    private async Task PopulatePersonalData()
    {
        var users = await queryProvider.Query<User>().ToListAsync();
    }

    private async Task PopulateFitnessPlans()
    {
        var users = await queryProvider.Query<User>().ToListAsync();
    }
}