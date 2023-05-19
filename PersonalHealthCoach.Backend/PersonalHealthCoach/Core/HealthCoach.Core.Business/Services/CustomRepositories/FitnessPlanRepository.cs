using HealthCoach.Core.Domain;
using HealthCoach.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthCoach.Core.Business;

public sealed class FitnessPlanRepository : IFitnessPlanRepository
{
    private readonly GenericDbContext dbContext;

    public FitnessPlanRepository(GenericDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<FitnessPlan> Load(Guid userId)
    {
        var plan = await dbContext.Set<FitnessPlan>()
            .Include(e => e.Workouts)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        var newWorkouts = new List<Workout>();
        foreach(var workout in plan.Workouts)
        {
            var db = await dbContext.Set<Workout>()
                .Include(w => w.Exercises)
                .FirstOrDefaultAsync(w => w.Id == workout.Id);

            newWorkouts.Add(workout);
        }
        plan.Workouts = newWorkouts;

        return plan;
    }
}
